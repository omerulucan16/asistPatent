using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class mgBankAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bankAccounts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    bankName = table.Column<string>(maxLength: 255, nullable: false),
                    accountCurrencyType = table.Column<int>(nullable: false),
                    brunchName = table.Column<string>(maxLength: 255, nullable: false),
                    brunchCode = table.Column<int>(maxLength: 50, nullable: false),
                    accountNumber = table.Column<int>(maxLength: 255, nullable: false),
                    ibanNo = table.Column<string>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankAccounts", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bankAccounts");
        }
    }
}
