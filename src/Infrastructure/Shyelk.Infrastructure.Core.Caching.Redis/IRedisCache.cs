using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Shyelk.Infrastructure.Core.Caching.Redis
{
    /// <summary>
    /// Redis缓存接口
    /// </summary>
    public interface IRedisCache
    {
         IDatabase GetDatabase(int db = -1, object asyncState = null);
         string Get(string key);
         Task<string> GetAsync(string key,CancellationToken cancellationToken=default(CancellationToken));
         bool Set(string key,string value,TimeSpan? keyTimeExpire=default(TimeSpan?));
         Task<bool> SetAsync(string key,string value,TimeSpan? keyTimeExpire=default(TimeSpan?),CancellationToken cancellationToken=default(CancellationToken));
         IDictionary<string,string> HashGet(string key,string[] fields);
         IDictionary<string,string> HashGetAll(string key);
         Task<IDictionary<string,string>> HashGetAllAsync(string key,CancellationToken cancellationToken=default(CancellationToken));
         Task<IDictionary<string,string>> HashGetAsync(string key,string[] fields,CancellationToken cancellationToken=default(CancellationToken));
         bool HashSet(string key,Dictionary<string,string> fields,TimeSpan? keyTimeExpire=default(TimeSpan?));
         Task<bool> HashSetAsync(string key,Dictionary<string,string> fields,TimeSpan? keyTimeExpire=default(TimeSpan?),CancellationToken cancellationToken=default(CancellationToken));
    }
}