using System;
using Microsoft.EntityFrameworkCore;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
namespace Shyelk.UserCenter.Entity
{
    public class EntityConfiguration : EntityTypeCofiguration
    {
        public override void ModelConfigurate(ModelBuilder builder)
        {
            builder.Entity<User>(u =>
            {
                u.HasKey(x => x.Id);
                u.HasIndex(x => new { x.Email, x.UserName,x.Sys_Status}).HasName("IDX_User");
                u.ToTable("S_User");
            });
            builder.Entity<Role>(r =>
            {
                r.HasKey(x => x.Id);
                r.HasIndex(x=>new{x.RoleName,x.Sys_Status}).HasName("IDX_Role");
                r.ToTable("S_Role");
            });
            builder.Entity<LoginHistory>(l =>
            {
                l.HasKey(x => x.Id);
                l.HasIndex(x=>x.Sys_Status).HasName("IDX_LH");
                l.HasOne(x => x.User).WithMany(x => x.LoginHistories).HasForeignKey(x => x.UserId);
                l.ToTable("S_LoginHistory");
            });
            builder.Entity<UserRole>(l =>
            {
                l.HasKey(x => x.Id).HasName("UserId");
                l.HasKey(x => x.RoleId).HasName("RoleId");
                l.HasIndex(x=>x.Sys_Status).HasName("IDX_UR");
                l.HasOne(x => x.Role).WithMany(r => r.Users).HasForeignKey(x => x.RoleId);
                l.HasOne(x => x.User).WithMany(u => u.Roles).HasForeignKey(x => x.Id);
                l.ToTable("S_UserRole");
            });
        }
    }
}