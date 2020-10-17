using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class mgBasketTotalPricendGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "basketGroupId",
                table: "baskets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "totalPrice",
                table: "baskets",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "basketGroupId",
                table: "baskets");

            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "baskets");
        }
    }
}
