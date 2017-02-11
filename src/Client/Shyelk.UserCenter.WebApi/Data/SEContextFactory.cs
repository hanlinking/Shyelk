using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
namespace Shyelk.UserCenter.WebApi.Data
{
    public class SEContextFactory : IDbContextFactory<SEDbContext>
    {
        public SEDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var Configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<SEDbContext>();
            optionsBuilder.UseMySql(Configuration.GetConnectionString("LogDb"), b => b.MigrationsAssembly("Shyelk.UserCenter.WebApi"));
            return new SEDbContext(optionsBuilder.Options);
        }
    }
}