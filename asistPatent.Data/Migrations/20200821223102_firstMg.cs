using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asistPatentCore.Data.Migrations
{
    public partial class firstMg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationClasses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    appclassnumber = table.Column<int>(nullable: false),
                    appclassname = table.Column<string>(maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationClasses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "brandApplicationTypes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false),
                    code = table.Column<string>(maxLength: 10, nullable: false),
                    applicationType = table.Column<int>(nullable: false),
                    categoryType = table.Column<int>(nullable: false),
                    userCreateDate = table.Column<DateTime>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brandApplicationTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "defaultValues",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    key = table.Column<string>(maxLength: 100, nullable: false),
                    status = table.Column<int>(nullable: false),
                    value = table.Column<string>(nullable: false),
                    keyTurkishText = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_defaultValues", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mailSources",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(maxLength: 255, nullable: false),
                    password = table.Column<string>(maxLength: 255, nullable: false),
                    smtpServer = table.Column<string>(maxLength: 255, nullable: false),
                    port = table.Column<int>(nullable: false),
                    enableSsl = table.Column<bool>(nullable: false),
                    displayName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mailSources", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<Guid>(nullable: false),
                    userName = table.Column<string>(maxLength: 120, nullable: true),
                    userSurname = table.Column<string>(maxLength: 120, nullable: true),
                    userEmailAdress = table.Column<string>(maxLength: 255, nullable: false),
                    userPassword = table.Column<string>(maxLength: 24, nullable: true),
                    userPhoneNumber = table.Column<string>(maxLength: 11, nullable: true),
                    status = table.Column<int>(nullable: false),
                    role = table.Column<int>(nullable: false),
                    userCreateDate = table.Column<DateTime>(nullable: false),
                    isBranch = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "applicationSubClasses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    subclasscode = table.Column<string>(maxLength: 25, nullable: false),
                    subclassname = table.Column<string>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    appclassId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationSubClasses", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationSubClasses_applicationClasses_appclassId",
                        column: x => x.appclassId,
                        principalTable: "applicationClasses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "brandApplicationVisiblities",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    applicationClassStatus = table.Column<bool>(nullable: false),
                    brandTitleChangeStatus = table.Column<bool>(nullable: false),
                    brandNameStatus = table.Column<bool>(nullable: false),
                    brandExplanationStatus = table.Column<bool>(nullable: false),
                    brandUploadStatus = table.Column<bool>(nullable: false),
                    companiesListStatus = table.Column<bool>(nullable: false),
                    brandApplicationTypesId = table.Column<int>(nullable: false),
                    userCreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brandApplicationVisiblities", x => x.id);
                    table.ForeignKey(
                        name: "FK_brandApplicationVisiblities_brandApplicationTypes_brandAppli~",
                        column: x => x.brandApplicationTypesId,
                        principalTable: "brandApplicationTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mailTemplates",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    mailHeader = table.Column<string>(nullable: false),
                    mailContent = table.Column<string>(nullable: false),
                    template = table.Column<int>(nullable: false),
                    sourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mailTemplates", x => x.id);
                    table.ForeignKey(
                        name: "FK_mailTemplates_mailSources_sourceId",
                        column: x => x.sourceId,
                        principalTable: "mailSources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "authorizedUsers",
                columns: table => new
                {
                    authId = table.Column<Guid>(nullable: false),
                    userName = table.Column<string>(maxLength: 100, nullable: false),
                    userSurname = table.Column<string>(maxLength: 100, nullable: false),
                    userEmailAdress = table.Column<string>(nullable: false),
                    userPhoneNumber = table.Column<string>(nullable: false),
                    userId = table.Column<Guid>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    userCreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedUsers", x => x.authId);
                    table.ForeignKey(
                        name: "FK_authorizedUsers_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "baskets",
                columns: table => new
                {
                    basketId = table.Column<Guid>(nullable: false),
                    appCategoryType = table.Column<int>(nullable: false),
                    appType = table.Column<int>(nullable: false),
                    brandName = table.Column<string>(maxLength: 255, nullable: true),
                    explation = table.Column<string>(maxLength: 255, nullable: true),
                    createDateTime = table.Column<DateTime>(nullable: false),
                    basketStatus = table.Column<int>(nullable: false),
                    brandAppTypeId = table.Column<int>(nullable: false),
                    userId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baskets", x => x.basketId);
                    table.ForeignKey(
                        name: "FK_baskets_brandApplicationTypes_brandAppTypeId",
                        column: x => x.brandAppTypeId,
                        principalTable: "brandApplicationTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_baskets_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    companyTitle = table.Column<string>(nullable: false),
                    adress = table.Column<string>(nullable: false),
                    taxCenter = table.Column<string>(maxLength: 255, nullable: true),
                    taxNumber = table.Column<int>(nullable: false),
                    companyType = table.Column<int>(nullable: false),
                    userId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.id);
                    table.ForeignKey(
                        name: "FK_companies_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usersTokens",
                columns: table => new
                {
                    tokenId = table.Column<Guid>(nullable: false),
                    createDate = table.Column<DateTime>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    userId = table.Column<Guid>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersTokens", x => x.tokenId);
                    table.ForeignKey(
                        name: "FK_usersTokens_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "applicationEmtiaClasses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    value = table.Column<string>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    appSubClassId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationEmtiaClasses", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationEmtiaClasses_applicationSubClasses_appSubClassId",
                        column: x => x.appSubClassId,
                        principalTable: "applicationSubClasses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "basketSubClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    appSubClassId = table.Column<int>(nullable: false),
                    basketId = table.Column<Guid>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    createDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basketSubClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_basketSubClasses_applicationSubClasses_appSubClassId",
                        column: x => x.appSubClassId,
                        principalTable: "applicationSubClasses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_basketSubClasses_baskets_basketId",
                        column: x => x.basketId,
                        principalTable: "baskets",
                        principalColumn: "basketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "basketCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    basketCompanyId = table.Column<int>(nullable: false),
                    basketId = table.Column<Guid>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    createDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basketCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_basketCompanies_companies_basketCompanyId",
                        column: x => x.basketCompanyId,
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_basketCompanies_baskets_basketId",
                        column: x => x.basketId,
                        principalTable: "baskets",
                        principalColumn: "basketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationEmtiaClasses_appSubClassId",
                table: "applicationEmtiaClasses",
                column: "appSubClassId");

            migrationBuilder.CreateIndex(
                name: "IX_applicationSubClasses_appclassId",
                table: "applicationSubClasses",
                column: "appclassId");

            migrationBuilder.CreateIndex(
                name: "IX_authorizedUsers_userId",
                table: "authorizedUsers",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_basketCompanies_basketCompanyId",
                table: "basketCompanies",
                column: "basketCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_basketCompanies_basketId",
                table: "basketCompanies",
                column: "basketId");

            migrationBuilder.CreateIndex(
                name: "IX_baskets_brandAppTypeId",
                table: "baskets",
                column: "brandAppTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_baskets_userId",
                table: "baskets",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_basketSubClasses_appSubClassId",
                table: "basketSubClasses",
                column: "appSubClassId");

            migrationBuilder.CreateIndex(
                name: "IX_basketSubClasses_basketId",
                table: "basketSubClasses",
                column: "basketId");

            migrationBuilder.CreateIndex(
                name: "IX_brandApplicationVisiblities_brandApplicationTypesId",
                table: "brandApplicationVisiblities",
                column: "brandApplicationTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_companies_userId",
                table: "companies",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_mailTemplates_sourceId",
                table: "mailTemplates",
                column: "sourceId");

            migrationBuilder.CreateIndex(
                name: "IX_usersTokens_userId",
                table: "usersTokens",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationEmtiaClasses");

            migrationBuilder.DropTable(
                name: "authorizedUsers");

            migrationBuilder.DropTable(
                name: "basketCompanies");

            migrationBuilder.DropTable(
                name: "basketSubClasses");

            migrationBuilder.DropTable(
                name: "brandApplicationVisiblities");

            migrationBuilder.DropTable(
                name: "defaultValues");

            migrationBuilder.DropTable(
                name: "mailTemplates");

            migrationBuilder.DropTable(
                name: "usersTokens");

            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropTable(
                name: "applicationSubClasses");

            migrationBuilder.DropTable(
                name: "baskets");

            migrationBuilder.DropTable(
                name: "mailSources");

            migrationBuilder.DropTable(
                name: "applicationClasses");

            migrationBuilder.DropTable(
                name: "brandApplicationTypes");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
