using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using Shyelk.Infrastructure.Core.Data.EntityFramework.Exceptions;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public sealed class SEDbContextManager
    {
        private SEDbContextManager(){}
        private static IDictionary<string, SEDbContextConfig> _dbContextConfigMap = new Dictionary<string, SEDbContextConfig>();
        private static ConcurrentDictionary<int, List<SEDbContextStorge>> _dbContextMap = new ConcurrentDictionary<int, List<SEDbContextStorge>>();
        private static Object LockObj = new Object();
        public static void Initial(string connectionName, string connectionString, DatabaseType type, string entitymap)
        {
            if (string.IsNullOrEmpty(connectionName))
            {
                throw new ArgumentNullException(nameof(connectionName));
            }
            if (string.IsNullOrEmpty(entitymap))
            {
                throw new ArgumentNullException(entitymap);
            }
            _dbContextConfigMap.Add(connectionName, new SEDbContextConfig(connectionString, type, entitymap));
        }
        internal static SEDbContext GetContext(string name)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            SEDbContextConfig dbContextStorge = null;
            List<SEDbContextStorge> dbContexts = null;
            SEDbContext dbContext = null;
            if (!_dbContextConfigMap.TryGetValue(name, out dbContextStorge))
            {
                throw new KeyNotFoundException(nameof(name));
            }

            if (!_dbContextMap.TryGetValue(threadId, out dbContexts))
            {
                dbContexts = new List<SEDbContextStorge>();
                dbContext = new SEDbContext(dbContextStorge.Connection, dbContextStorge.EntityMapper, dbContextStorge.Type);
                dbContexts.Add(new SEDbContextStorge(name, dbContext));
                _dbContextMap.TryAdd(threadId, dbContexts);
            }
            else
            {
                var contextStorge = dbContexts.FirstOrDefault(f => f.Name == name);
                if (contextStorge == null)
                {
                    dbContext = new SEDbContext(dbContextStorge.Connection, dbContextStorge.EntityMapper, dbContextStorge.Type);
                    dbContexts.Add(new SEDbContextStorge(name, dbContext));
                    _dbContextMap.TryAdd(threadId, dbContexts);
                }else
                {
                    dbContext=contextStorge.Context;
                }
            }
            return dbContext;
        }
        internal static SEDbContext GetContext()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            SEDbContextConfig dbContextStorge = _dbContextConfigMap.FirstOrDefault().Value;
            if (dbContextStorge==null)
            {
                throw new SEDbContextException("DbContext Not Config");
            }
            List<SEDbContextStorge> dbContexts = null;
            SEDbContext dbContext = null;

            if (!_dbContextMap.TryGetValue(threadId, out dbContexts))
            {
                dbContexts = new List<SEDbContextStorge>();
                dbContext = new SEDbContext(dbContextStorge.Connection, dbContextStorge.EntityMapper, dbContextStorge.Type);
                dbContexts.Add(new SEDbContextStorge("", dbContext));
                _dbContextMap.TryAdd(threadId, dbContexts);
            }
            else
            {
                var contextStorge = dbContexts.FirstOrDefault();
                if (contextStorge == null)
                {
                    dbContext = new SEDbContext(dbContextStorge.Connection, dbContextStorge.EntityMapper, dbContextStorge.Type);
                    dbContexts.Add(new SEDbContextStorge("", dbContext));
                    _dbContextMap.TryAdd(threadId, dbContexts);
                }else
                {
                    dbContext=contextStorge.Context;
                }
            }
            return dbContext;
        }
       public static void Dispose()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            List<SEDbContextStorge> SEDbContextStorgeList = null;
            _dbContextMap.TryRemove(threadId, out SEDbContextStorgeList);
        }
    }
    public enum DatabaseType
    {
        Sqlserver = 0,
        MySql = 1,
        Sqllite = 2
    }
}
