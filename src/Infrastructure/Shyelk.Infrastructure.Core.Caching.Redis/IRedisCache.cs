using StackExchange.Redis;
using System;
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
    }
}