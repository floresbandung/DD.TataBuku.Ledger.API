using Microsoft.EntityFrameworkCore.Migrations;

namespace DD.TataBuku.Ledger.API.Migrations
{
    public partial class addaccountcodeGL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountCode",
                table: "GeneralLedgerDetails",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "GeneralLedgerDetails",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountCode",
                table: "GeneralLedgerDetails");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "GeneralLedgerDetails");
        }
    }
}
