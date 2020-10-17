using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class mgAgreementsRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgreementsRoles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<Guid>(nullable: false),
                    agreementsStatusesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementsRoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_AgreementsRoles_agreementStatuses_agreementsStatusesId",
                        column: x => x.agreementsStatusesId,
                        principalTable: "agreementStatuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgreementsRoles_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementsRoles_agreementsStatusesId",
                table: "AgreementsRoles",
                column: "agreementsStatusesId");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementsRoles_userId",
                table: "AgreementsRoles",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgreementsRoles");
        }
    }
}
