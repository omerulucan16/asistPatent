using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class agreementRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgreementsUsers");

            migrationBuilder.CreateTable(
                name: "agreementsRoles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<Guid>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agreementsRoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_agreementsRoles_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agreementsRoles_userId",
                table: "agreementsRoles",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agreementsRoles");

            migrationBuilder.CreateTable(
                name: "AgreementsUsers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsersuserId = table.Column<string>(type: "char(36)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementsUsers", x => x.id);
                    table.ForeignKey(
                        name: "FK_AgreementsUsers_users_UsersuserId",
                        column: x => x.UsersuserId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementsUsers_UsersuserId",
                table: "AgreementsUsers",
                column: "UsersuserId");
        }
    }
}
