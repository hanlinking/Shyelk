using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Shyelk.Infrastructure.Core.Caching.Redis
{
    public class RedisCache : IRedisCache
    {
        // KEYS[1] = = key
        // ARGV[1] = absolute-expiration - ticks as long (-1 for none)
        // ARGV[2] = sliding-expiration - ticks as long (-1 for none)
        // ARGV[3] = relative-expiration (long, in seconds, -1 for none) - Min(absolute-expiration - Now, sliding-expiration)
        // ARGV[4] = data - byte[]
        // this order should not change LUA script depends on it
        private const string STR_SET_SCRIPT = @"
            redis.call('SET',KEYS[1],ARGV[1])
            if ARGV[2]~='-1' then
                redis.call('EXPIRE',KEYS[1],ARGV[2])
            end
            return 1";

        private static ConnectionMultiplexer _connection;

        private readonly RedisCacheOptions _options;
        private readonly string _instance;
        private long GetTimeExpire(TimeSpan? timeExpire)
        {
            long time = -1;
            if (timeExpire.HasValue)
            {
                time = (long)Math.Floor(timeExpire.Value.TotalSeconds);
            }
            return time;
        }
        protected virtual ConnectionMultiplexer Connection
        {
            get
            {
                if (_connection == null || !_connection.IsConnected)
                {
                    Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                    {
                        return ConnectionMultiplexer.Connect(_options.Configuration);
                    });
                    _connection = LazyConnection.Value;
                }
                return _connection;
            }
        }

        protected virtual IDatabase _cache
        {
            get { return Connection.GetDatabase(); }
        }
        public RedisCache(IOptions<RedisCacheOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.Value;

            // This allows partitioning a single backend cache for use with multiple apps/services.
            _instance = _options.InstanceName ?? string.Empty;
        }

        public IDatabase GetDatabase(int db = -1, object asyncState = null)
        {
            return _cache;
        }


        public string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            RedisValue result = _cache.StringGet(key);
            return result;
        }

        public bool Set(string key, string value, TimeSpan? keyTimeExpire = default(TimeSpan?))
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            RedisResult result = _cache.ScriptEvaluate(STR_SET_SCRIPT, new RedisKey[] { key }, new RedisValue[]{
                    value,
                    GetTimeExpire(keyTimeExpire)
            });
            return !result.IsNull;
        }

        public Task<string> GetAsync(string key, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            return Task.Factory.StartNew(() =>
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException(nameof(key));
                }
                RedisValue result = _cache.StringGet(key);
                return (string)result;
            }, cancellationToken);
        }

        public Task<bool> SetAsync(string key, string value, TimeSpan? keyTimeExpire = default(TimeSpan?), CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            return Task.Factory.StartNew(() =>
            {
                RedisResult result = _cache.ScriptEvaluate(STR_SET_SCRIPT, new RedisKey[] { key }, new RedisValue[]{
                    value,
                    GetTimeExpire(keyTimeExpire)
                });
                return !result.IsNull;
            }, cancellationToken);
        }

        public IDictionary<string, string> HashGet(string key, string[] fields)
        {
            return _hashGet(key, fields);
        }

        public Task<IDictionary<string, string>> HashGetAsync(string key, string[] fields, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            return Task.Factory.StartNew(() =>
             {
                 return _hashGet(key, fields);
             }, cancellationToken);
        }
        public IDictionary<string, string> HashGetAll(string key)
        {
            return _hashGet(key, null);
        }

        public Task<IDictionary<string, string>> HashGetAllAsync(string key, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            return Task.Factory.StartNew(() =>
             {
                 return _hashGet(key, null);
             }, cancellationToken);
        }
        public bool HashSet(string key, Dictionary<string, string> fields, TimeSpan? keyTimeExpire = default(TimeSpan?))
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            ITransaction trans = _cache.CreateTransaction();
            trans.HashSetAsync(key, fields.Select(s => new HashEntry(s.Key, s.Value)).ToArray());
            if (keyTimeExpire.HasValue)
            {
                trans.KeyExpireAsync(key, keyTimeExpire.Value);
            }
            return trans.Execute();
        }

        public Task<bool> HashSetAsync(string key, Dictionary<string, string> fields, TimeSpan? keyTimeExpire = default(TimeSpan?), CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            ITransaction trans = _cache.CreateTransaction();
            trans.HashSetAsync(key, fields.Select(s => new HashEntry(s.Key, s.Value)).ToArray());
            if (keyTimeExpire.HasValue)
            {
                trans.KeyExpireAsync(key, keyTimeExpire.Value);
            }
            return trans.ExecuteAsync();
        }

        private IDictionary<string, string> _hashGet(string key, string[] fields)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            Dictionary<string, string> result = null;
            if (fields == null || fields.Count() == 0)
            {
                var redisresult = _cache.HashGetAll(key);
                if (redisresult.Any(r => r.Value.HasValue))
                {
                    result = redisresult.ToDictionary(k => (string)k.Name, v => (string)v.Value);
                }
            }
            else
            {
                var redisresult = _cache.HashGet(key, fields.Select(s => (RedisValue)s).ToArray());
                if (redisresult.Any(r => r.HasValue))
                {
                    result = new Dictionary<string, string>();
                    for (int i = 0; i < fields.Count(); i++)
                    {
                        result.Add(fields[i], redisresult[i]);
                    }
                }
            }
            return result;
        }


    }
}