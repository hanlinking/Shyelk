using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shyelk.Infrastructure.Core.Caching
{
    public interface IShyelkCache
    {

        object Get(string Key);
        bool Set(string key, object value,ShyelkCacheEntryOption options);
        bool Remove(string key);
        bool RemoveAsync(string key);
        object GetAsync(string Key);
        bool SetAsync(string key, object value);
        
    }
}
