using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public sealed class SEDbContextManager
    {
        private SEDbContextManager() { }
        private static IDictionary<string, SEDbContextConfig> _dbContextConfigMap = new Dictionary<string, SEDbContextConfig>();
        [ThreadStaticAttribute]
        private static List<SEDbContextStorge> SEDbContextStorgeList;
        private static AsyncLocal<List<SEDbContextStorge>> _asyncLocalStorge=new AsyncLocal<List<SEDbContextStorge>>();
        public static void Initial(string connectionName, string connectionString, DatabaseType type, string[] assemblyNames)
        {
            if (string.IsNullOrEmpty(connectionName))
            {
                throw new ArgumentNullException(nameof(connectionName));
            }
            List<Assembly> entityMapperAssmeblies = null;
            if (assemblyNames != null && assemblyNames.Length > 0)
            {
                entityMapperAssmeblies = new List<Assembly>();
                foreach (var name in assemblyNames)
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(name));
                    entityMapperAssmeblies.Add(assembly);
                }
            }
            _dbContextConfigMap.Add(connectionName, new SEDbContextConfig(connectionString, type, entityMapperAssmeblies.ToArray()));
        }
        internal static SEDbContext GetDbContext(string name)
        {
            SEDbContextConfig dbContextConfig = null;
            if (string.IsNullOrEmpty(name))
            {
                name="default";
                dbContextConfig = _dbContextConfigMap.FirstOrDefault().Value;
            }
            else if (!_dbContextConfigMap.TryGetValue(name, out dbContextConfig))
            {
                throw new KeyNotFoundException(nameof(name));
            }
            if (_asyncLocalStorge.Value==null||_asyncLocalStorge.Value.Count() == 0)
            {
                _asyncLocalStorge.Value=new List<SEDbContextStorge>();
                SEDbContext dbContext = new SEDbContext(dbContextConfig.Connection, dbContextConfig.EntityMapper, dbContextConfig.Type);
                SEDbContextStorge dbcontextStorge = new SEDbContextStorge(name, dbContext);
                _asyncLocalStorge.Value.Add(dbcontextStorge);
                return dbContext;
            }
            else
            {
                var context = _asyncLocalStorge.Value.FirstOrDefault(f=>f.Name==name).Context;
                if (context == null)
                {
                    context = new SEDbContext(dbContextConfig.Connection, dbContextConfig.EntityMapper, dbContextConfig.Type);
                    SEDbContextStorge dbcontextStorge = new SEDbContextStorge(name, context);
                    _asyncLocalStorge.Value.Add(dbcontextStorge);
                }
                return context;
            }
        }
        internal static SEDbContext GetDbContext()
        {
           return GetDbContext("");
        }
    }
    public enum DatabaseType
    {
        Sqlserver = 0,
        MySql = 1,
        Sqlite = 2
    }
}
