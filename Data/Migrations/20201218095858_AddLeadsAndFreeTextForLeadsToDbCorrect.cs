using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesCRM.Data.Migrations
{
    public partial class AddLeadsAndFreeTextForLeadsToDbCorrect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 9, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Phonenumber = table.Column<string>(nullable: false),
                    SecondaryPhonenumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Male = table.Column<bool>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    SafetyCheck = table.Column<bool>(nullable: false, defaultValue: false),
                    PaidRegistrationFee = table.Column<bool>(nullable: false, defaultValue: false),
                    SignedContract = table.Column<bool>(nullable: false, defaultValue: false),
                    AssignedToWork = table.Column<bool>(nullable: false, defaultValue: false),
                    PaymentTransfered = table.Column<bool>(nullable: false, defaultValue: false),
                    DesiredCourse = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FreeTexts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: false),
                    LeadID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeTexts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FreeTexts_Leads_LeadID",
                        column: x => x.LeadID,
                        principalTable: "Leads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FreeTexts_LeadID",
                table: "FreeTexts",
                column: "LeadID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreeTexts");

            migrationBuilder.DropTable(
                name: "Leads");
        }
    }
}
