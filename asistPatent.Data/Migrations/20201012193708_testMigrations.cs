using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class testMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agreementsRoles");

            migrationBuilder.DropColumn(
                name: "level",
                table: "agreementStatuses");

            migrationBuilder.DropColumn(
                name: "nextLevel",
                table: "agreementStatuses");

            migrationBuilder.DropColumn(
                name: "status",
                table: "agreements");

            migrationBuilder.AddColumn<int>(
                name: "statusId",
                table: "agreements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_agreements_statusId",
                table: "agreements",
                column: "statusId");

            migrationBuilder.AddForeignKey(
                name: "FK_agreements_agreementStatuses_statusId",
                table: "agreements",
                column: "statusId",
                principalTable: "agreementStatuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agreements_agreementStatuses_statusId",
                table: "agreements");

            migrationBuilder.DropIndex(
                name: "IX_agreements_statusId",
                table: "agreements");

            migrationBuilder.DropColumn(
                name: "statusId",
                table: "agreements");

            migrationBuilder.AddColumn<int>(
                name: "level",
                table: "agreementStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "nextLevel",
                table: "agreementStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "agreements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "agreementsRoles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "char(36)", nullable: false)
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
    }
}
