using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class mgAgreementsResponsibleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "responsibleUserId",
                table: "agreements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_agreements_responsibleUserId",
                table: "agreements",
                column: "responsibleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_agreements_users_responsibleUserId",
                table: "agreements",
                column: "responsibleUserId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agreements_users_responsibleUserId",
                table: "agreements");

            migrationBuilder.DropIndex(
                name: "IX_agreements_responsibleUserId",
                table: "agreements");

            migrationBuilder.DropColumn(
                name: "responsibleUserId",
                table: "agreements");
        }
    }
}
