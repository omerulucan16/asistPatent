using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class mgApplicationPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brandApplicationPrices",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    brandAppTypeId = table.Column<int>(nullable: false),
                    tuition = table.Column<decimal>(nullable: false),
                    service = table.Column<decimal>(nullable: false),
                    branch = table.Column<decimal>(nullable: false),
                    userCreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brandApplicationPrices", x => x.id);
                    table.ForeignKey(
                        name: "FK_brandApplicationPrices_brandApplicationTypes_brandAppTypeId",
                        column: x => x.brandAppTypeId,
                        principalTable: "brandApplicationTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_brandApplicationPrices_brandAppTypeId",
                table: "brandApplicationPrices",
                column: "brandAppTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "brandApplicationPrices");
        }
    }
}
