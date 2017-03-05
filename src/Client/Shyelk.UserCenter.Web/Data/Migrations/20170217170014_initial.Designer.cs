using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Shyelk.Infrastructure.Core.Data.EntityFramework;

namespace Shyelk.UserCenter.Web.Migrations
{
    [DbContext(typeof(SEDbContext))]
    [Migration("20170217170014_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Shyelk.UserCenter.Entity.LoginHistory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ipv4")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 16);

                    b.Property<string>("Ipv6")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 46);

                    b.Property<string>("Location");

                    b.Property<DateTime>("Sys_CreateTime");

                    b.Property<DateTime>("Sys_CreateTimeUtc");

                    b.Property<string>("Sys_Creator");

                    b.Property<string>("Sys_DataSource");

                    b.Property<string>("Sys_Modifier");

                    b.Property<DateTime?>("Sys_ModifyTime");

                    b.Property<DateTime?>("Sys_ModifyTimeUtc");

                    b.Property<bool>("Sys_Status");

                    b.Property<byte[]>("Sys_Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("SystemId");

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.HasKey("Id");

                    b.HasIndex("Sys_Status")
                        .HasName("IDX_LH");

                    b.HasIndex("UserId");

                    b.ToTable("S_LoginHistory");
                });

            modelBuilder.Entity("Shyelk.UserCenter.Entity.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 18);

                    b.Property<DateTime>("Sys_CreateTime");

                    b.Property<DateTime>("Sys_CreateTimeUtc");

                    b.Property<string>("Sys_Creator");

                    b.Property<string>("Sys_DataSource");

                    b.Property<string>("Sys_Modifier");

                    b.Property<DateTime?>("Sys_ModifyTime");

                    b.Property<DateTime?>("Sys_ModifyTimeUtc");

                    b.Property<bool>("Sys_Status");

                    b.Property<byte[]>("Sys_Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("RoleName", "Sys_Status")
                        .HasName("IDX_Role");

                    b.ToTable("S_Role");
                });

            modelBuilder.Entity("Shyelk.UserCenter.Entity.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("HeaderUrl");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Phone");

                    b.Property<string>("SecurityCode")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("Summary");

                    b.Property<DateTime>("Sys_CreateTime");

                    b.Property<DateTime>("Sys_CreateTimeUtc");

                    b.Property<string>("Sys_Creator");

                    b.Property<string>("Sys_DataSource");

                    b.Property<string>("Sys_Modifier");

                    b.Property<DateTime?>("Sys_ModifyTime");

                    b.Property<DateTime?>("Sys_ModifyTimeUtc");

                    b.Property<bool>("Sys_Status");

                    b.Property<byte[]>("Sys_Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.HasIndex("Email", "UserName", "Sys_Status")
                        .HasName("IDX_User");

                    b.ToTable("S_User");
                });

            modelBuilder.Entity("Shyelk.UserCenter.Entity.UserRole", b =>
                {
                    b.Property<string>("RoleId");

                    b.Property<string>("Id")
                        .IsRequired();

                    b.Property<DateTime>("Sys_CreateTime");

                    b.Property<DateTime>("Sys_CreateTimeUtc");

                    b.Property<string>("Sys_Creator");

                    b.Property<string>("Sys_DataSource");

                    b.Property<string>("Sys_Modifier");

                    b.Property<DateTime?>("Sys_ModifyTime");

                    b.Property<DateTime?>("Sys_ModifyTimeUtc");

                    b.Property<bool>("Sys_Status");

                    b.Property<byte[]>("Sys_Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("RoleId")
                        .HasName("RoleId");

                    b.HasAlternateKey("Id")
                        .HasName("UserId");

                    b.HasIndex("Sys_Status")
                        .HasName("IDX_UR");

                    b.ToTable("S_UserRole");
                });

            modelBuilder.Entity("Shyelk.UserCenter.Entity.LoginHistory", b =>
                {
                    b.HasOne("Shyelk.UserCenter.Entity.User", "User")
                        .WithMany("LoginHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Shyelk.UserCenter.Entity.UserRole", b =>
                {
                    b.HasOne("Shyelk.UserCenter.Entity.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Shyelk.UserCenter.Entity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
