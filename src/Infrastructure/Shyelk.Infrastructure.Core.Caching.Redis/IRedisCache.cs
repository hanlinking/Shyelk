using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
namespace Shyelk.Infrastructure.Core.Caching.Redis
{
    /// <summary>
    /// Redis缓存接口
    /// </summary>
    public interface IRedisCache:IDistributedCache
    {
         IDatabase GetDatabase(int db = -1, object asyncState = null);
    }
}