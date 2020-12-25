using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesCRM.Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeadID1",
                table: "FreeTexts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FreeTexts_LeadID1",
                table: "FreeTexts",
                column: "LeadID1");

            migrationBuilder.AddForeignKey(
                name: "FK_FreeTexts_Leads_LeadID1",
                table: "FreeTexts",
                column: "LeadID1",
                principalTable: "Leads",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreeTexts_Leads_LeadID1",
                table: "FreeTexts");

            migrationBuilder.DropIndex(
                name: "IX_FreeTexts_LeadID1",
                table: "FreeTexts");

            migrationBuilder.DropColumn(
                name: "LeadID1",
                table: "FreeTexts");
        }
    }
}
