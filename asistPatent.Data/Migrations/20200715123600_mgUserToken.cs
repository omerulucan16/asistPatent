using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class mgUserToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usersTokens",
                columns: table => new
                {
                    tokenId = table.Column<Guid>(nullable: false),
                    createDate = table.Column<DateTime>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    userId = table.Column<Guid>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersTokens", x => x.tokenId);
                    table.ForeignKey(
                        name: "FK_usersTokens_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usersTokens_userId",
                table: "usersTokens",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usersTokens");
        }
    }
}
