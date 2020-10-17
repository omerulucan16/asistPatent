using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class mgAgreementsResponsibleNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agreements_users_responsibleUserId",
                table: "agreements");

            migrationBuilder.AlterColumn<Guid>(
                name: "responsibleUserId",
                table: "agreements",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_agreements_users_responsibleUserId",
                table: "agreements",
                column: "responsibleUserId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agreements_users_responsibleUserId",
                table: "agreements");

            migrationBuilder.AlterColumn<Guid>(
                name: "responsibleUserId",
                table: "agreements",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_agreements_users_responsibleUserId",
                table: "agreements",
                column: "responsibleUserId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
