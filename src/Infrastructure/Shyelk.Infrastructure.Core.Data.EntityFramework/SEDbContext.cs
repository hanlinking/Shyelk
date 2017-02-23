using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Loader;
using Shyelk.Infrastructure.Core.Reflection;
using System.Reflection;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public class SEDbContext : DbContext
    {
        protected readonly string ConnectionString;
        protected readonly Assembly[] MapperAssembly;
        protected readonly DatabaseType Type;
        public SEDbContext(DbContextOptions options) : base(options)
        {

        }
        internal SEDbContext(string connetionString, Assembly[] mapperAssembly, DatabaseType type = DatabaseType.Sqlserver) : base()
        {
            MapperAssembly = mapperAssembly;
            ConnectionString = connetionString;
            Type = type;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                switch (Type)
                {
                    case DatabaseType.Sqlserver:
                        optionsBuilder.UseSqlServer(ConnectionString);
                        break;
                    case DatabaseType.Sqllite:
                        optionsBuilder.UseSqlite(ConnectionString);
                        break;
                    case DatabaseType.MySql:
                        optionsBuilder.UseMySql(ConnectionString);
                        break;
                    default:
                        optionsBuilder.UseSqlServer(ConnectionString);
                        break;
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            string assemblyPath = string.Empty;
            IEnumerable<Type> types = null;
            if (MapperAssembly!=null&&MapperAssembly.Length>0)
            {
                types = ReflectionTools.GetSubTypes<EntityTypeCofiguration>(MapperAssembly);
            }
            if (types == null)
            {
                types = ReflectionTools.GetSubTypes<EntityTypeCofiguration>();
            }
            foreach (var type in types)
            {
                var instance = ReflectionTools.CreateInstance(type) as EntityTypeCofiguration;
                instance.ModelConfigurate(builder);
            }
        }
    }
}
