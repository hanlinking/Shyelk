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
        private const string SetScript = (@"
                redis.call('HMSET', KEYS[1], 'absexp', ARGV[1], 'sldexp', ARGV[2], 'data', ARGV[4])
                if ARGV[3] ~= '-1' then
                  redis.call('EXPIRE', KEYS[1], ARGV[3])
                end
                return 1");
        private const string STRSETSCRIPT = @"
            redis.call('SET',KEYS[1],ARGV[1])
            if ARGV[2]~='-1' then
                redis.call('EXPIRE',KEYS[1],ARGV[2])
            end
            return 1";
        private const string AbsoluteExpirationKey = "absexp";
        private const string SlidingExpirationKey = "sldexp";
        private const string DataKey = "data";
        private const long NotPresent = -1;

        private static ConnectionMultiplexer _connection;
        protected ConnectionMultiplexer Connection
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
        private IDatabase _cache;

        private readonly RedisCacheOptions _options;
        private readonly string _instance;

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

        private void Connect()
        {
            lock (this)
            {
                _cache = Connection.GetDatabase();
            }
        }

        private async Task ConnectAsync()
        {
            if (_connection == null)
            {
                try
                {
                    _connection = await ConnectionMultiplexer.ConnectAsync(_options.Configuration);
                }
                catch (RedisConnectionException ex)
                {
                    throw ex;
                }

            }
            _cache = _connection.GetDatabase();
        }

        public IDatabase GetDatabase(int db = -1, object asyncState = null)
        {
            Connect();
            return _cache;
        }


        public string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            Connect();
            RedisValue result = _cache.StringGet(key);
            return result;
        }

        public bool Set(string key, string value, TimeSpan? keyTimeExpire = default(TimeSpan?))
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            Connect();
            RedisResult result = _cache.ScriptEvaluate(STRSETSCRIPT, new RedisKey[] { key }, new RedisValue[]{
                    value,
                    GetTimeExpire(keyTimeExpire)
            });
            return !result.IsNull;
        }
        private long GetTimeExpire(TimeSpan? timeExpire)
        {
            long time = -1;
            if (timeExpire.HasValue)
            {
                time = (long)Math.Floor(timeExpire.Value.TotalSeconds);
            }
            return time;
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
                Connect();
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
                Connect();
                RedisResult result = _cache.ScriptEvaluate(STRSETSCRIPT, new RedisKey[] { key }, new RedisValue[]{
                    value,
                    GetTimeExpire(keyTimeExpire)
                });
                return !result.IsNull;
            },cancellationToken);
        }
    }
}