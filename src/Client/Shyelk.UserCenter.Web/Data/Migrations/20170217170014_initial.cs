using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shyelk.UserCenter.Web.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "S_Role",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    RoleName = table.Column<string>(maxLength: 18, nullable: false),
                    Sys_CreateTime = table.Column<DateTime>(nullable: false),
                    Sys_CreateTimeUtc = table.Column<DateTime>(nullable: false),
                    Sys_Creator = table.Column<string>(nullable: true),
                    Sys_DataSource = table.Column<string>(nullable: true),
                    Sys_Modifier = table.Column<string>(nullable: true),
                    Sys_ModifyTime = table.Column<DateTime>(nullable: true),
                    Sys_ModifyTimeUtc = table.Column<DateTime>(nullable: true),
                    Sys_Status = table.Column<bool>(nullable: false),
                    Sys_Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGeneratedOnAddOrUpdate", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "S_User",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Email = table.Column<string>(nullable: false),
                    HeaderUrl = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    SecurityCode = table.Column<string>(maxLength: 128, nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Sys_CreateTime = table.Column<DateTime>(nullable: false),
                    Sys_CreateTimeUtc = table.Column<DateTime>(nullable: false),
                    Sys_Creator = table.Column<string>(nullable: true),
                    Sys_DataSource = table.Column<string>(nullable: true),
                    Sys_Modifier = table.Column<string>(nullable: true),
                    Sys_ModifyTime = table.Column<DateTime>(nullable: true),
                    Sys_ModifyTimeUtc = table.Column<DateTime>(nullable: true),
                    Sys_Status = table.Column<bool>(nullable: false),
                    Sys_Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGeneratedOnAddOrUpdate", true),
                    UserName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "S_LoginHistory",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Ipv4 = table.Column<string>(maxLength: 16, nullable: false),
                    Ipv6 = table.Column<string>(maxLength: 46, nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Sys_CreateTime = table.Column<DateTime>(nullable: false),
                    Sys_CreateTimeUtc = table.Column<DateTime>(nullable: false),
                    Sys_Creator = table.Column<string>(nullable: true),
                    Sys_DataSource = table.Column<string>(nullable: true),
                    Sys_Modifier = table.Column<string>(nullable: true),
                    Sys_ModifyTime = table.Column<DateTime>(nullable: true),
                    Sys_ModifyTimeUtc = table.Column<DateTime>(nullable: true),
                    Sys_Status = table.Column<bool>(nullable: false),
                    Sys_Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGeneratedOnAddOrUpdate", true),
                    SystemId = table.Column<Guid>(nullable: false),
                    SystemName = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S_LoginHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_S_LoginHistory_S_User_UserId",
                        column: x => x.UserId,
                        principalTable: "S_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "S_UserRole",
                columns: table => new
                {
                    RoleId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Sys_CreateTime = table.Column<DateTime>(nullable: false),
                    Sys_CreateTimeUtc = table.Column<DateTime>(nullable: false),
                    Sys_Creator = table.Column<string>(nullable: true),
                    Sys_DataSource = table.Column<string>(nullable: true),
                    Sys_Modifier = table.Column<string>(nullable: true),
                    Sys_ModifyTime = table.Column<DateTime>(nullable: true),
                    Sys_ModifyTimeUtc = table.Column<DateTime>(nullable: true),
                    Sys_Status = table.Column<bool>(nullable: false),
                    Sys_Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGeneratedOnAddOrUpdate", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RoleId", x => x.RoleId);
                    table.UniqueConstraint("UserId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_S_UserRole_S_User_Id",
                        column: x => x.Id,
                        principalTable: "S_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_S_UserRole_S_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "S_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_LH",
                table: "S_LoginHistory",
                column: "Sys_Status");

            migrationBuilder.CreateIndex(
                name: "IX_S_LoginHistory_UserId",
                table: "S_LoginHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IDX_Role",
                table: "S_Role",
                columns: new[] { "RoleName", "Sys_Status" });

            migrationBuilder.CreateIndex(
                name: "IDX_User",
                table: "S_User",
                columns: new[] { "Email", "UserName", "Sys_Status" });

            migrationBuilder.CreateIndex(
                name: "IDX_UR",
                table: "S_UserRole",
                column: "Sys_Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "S_LoginHistory");

            migrationBuilder.DropTable(
                name: "S_UserRole");

            migrationBuilder.DropTable(
                name: "S_User");

            migrationBuilder.DropTable(
                name: "S_Role");
        }
    }
}
