using System;
using Microsoft.EntityFrameworkCore;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
namespace Shyelk.UserCenter.Entity
{
    public class EntityConfiguration : EntityTypeCofiguration
    {
        public EntityConfiguration() { }
        public override void ModelConfigurate(ModelBuilder builder)
        {
            builder.Entity<User>(u =>
            {
                u.HasKey(x => x.Id);
                u.ToTable("S_User");
            });
            builder.Entity<Role>(r =>
            {
                r.HasKey(x => x.Id);
            });
        }
    }
}