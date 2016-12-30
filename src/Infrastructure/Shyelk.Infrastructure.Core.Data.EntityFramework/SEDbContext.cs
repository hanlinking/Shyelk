using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shyelk.Infrastructure.Core.Reflection;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    public class SEDbContext : DbContext
    {
        protected string _ConnectionString;
        public SEDbContext(DbContextOptions options) : base(options)
        {
        }
        public SEDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
