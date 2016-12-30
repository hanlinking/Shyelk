using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public class SEDbContextManager:IDisposable
    {
        private static IDictionary<string, SEDbContext> _dbContextMap = new Dictionary<string, SEDbContext>();
        public static void Initial(string connectionName, DatabaseType type,string entitymap)
        {
            if (string.IsNullOrEmpty(connectionName))
            {
                throw new ArgumentNullException(nameof(connectionName));
            }
            if (string.IsNullOrEmpty(entitymap))
            {
                throw new ArgumentNullException(entitymap);
            }
            _dbContextMap.Add(connectionName,new SEDbContext());
        }
        public static SEDbContext GetContext(string name)
        {
            SEDbContext dbcontext=null;
           if (!_dbContextMap.TryGetValue(name,out dbcontext))
           {
               throw new KeyNotFoundException(nameof(name));
           } 
           return dbcontext;
        }
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
    public enum DatabaseType
    {
        Sqlserver = 0,
        MySql = 1,
        Sqllite = 2
    }
}
