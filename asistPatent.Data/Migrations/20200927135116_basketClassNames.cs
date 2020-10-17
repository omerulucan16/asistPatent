using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class basketClassNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "basketSubClassName",
                table: "basketSubClasses",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "basketClassName",
                table: "basketClass",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "basketSubClassName",
                table: "basketSubClasses");

            migrationBuilder.DropColumn(
                name: "basketClassName",
                table: "basketClass");
        }
    }
}
