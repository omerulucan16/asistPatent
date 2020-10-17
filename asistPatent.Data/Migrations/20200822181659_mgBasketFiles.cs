using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class mgBasketFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "basketFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    filename = table.Column<string>(nullable: true),
                    basketId = table.Column<Guid>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    createDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basketFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_basketFiles_baskets_basketId",
                        column: x => x.basketId,
                        principalTable: "baskets",
                        principalColumn: "basketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_basketFiles_basketId",
                table: "basketFiles",
                column: "basketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basketFiles");
        }
    }
}
