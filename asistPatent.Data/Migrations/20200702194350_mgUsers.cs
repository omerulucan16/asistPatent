using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class mgUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<Guid>(nullable: false),
                    userName = table.Column<string>(maxLength: 120, nullable: true),
                    userSurname = table.Column<string>(maxLength: 120, nullable: true),
                    userEmailAdress = table.Column<string>(maxLength: 255, nullable: false),
                    userPassword = table.Column<string>(maxLength: 24, nullable: true),
                    userPhoneNumber = table.Column<string>(maxLength: 11, nullable: true),
                    accessToken = table.Column<string>(maxLength: 255, nullable: true),
                    isRegistered = table.Column<bool>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    userCreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
