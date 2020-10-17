using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class basketClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "tuition",
                table: "brandApplicationPrices",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<double>(
                name: "service",
                table: "brandApplicationPrices",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<double>(
                name: "extraClassService",
                table: "brandApplicationPrices",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<double>(
                name: "branch",
                table: "brandApplicationPrices",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<double>(
                name: "totalPrice",
                table: "baskets",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.CreateTable(
                name: "basketClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    appClassId = table.Column<int>(nullable: false),
                    basketId = table.Column<Guid>(nullable: false),
                    additionalValue = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false),
                    createDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basketClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_basketClass_applicationClasses_appClassId",
                        column: x => x.appClassId,
                        principalTable: "applicationClasses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_basketClass_baskets_basketId",
                        column: x => x.basketId,
                        principalTable: "baskets",
                        principalColumn: "basketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_basketClass_appClassId",
                table: "basketClass",
                column: "appClassId");

            migrationBuilder.CreateIndex(
                name: "IX_basketClass_basketId",
                table: "basketClass",
                column: "basketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basketClass");

            migrationBuilder.AlterColumn<decimal>(
                name: "tuition",
                table: "brandApplicationPrices",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "service",
                table: "brandApplicationPrices",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "extraClassService",
                table: "brandApplicationPrices",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "branch",
                table: "brandApplicationPrices",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "totalPrice",
                table: "baskets",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
