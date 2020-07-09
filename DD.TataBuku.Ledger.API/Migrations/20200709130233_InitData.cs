using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DD.TataBuku.Ledger.API.Migrations
{
    public partial class InitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralLedgers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowStatus = table.Column<byte>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 24, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 24, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    GeneralLedgerNo = table.Column<string>(maxLength: 24, nullable: false),
                    GeneralLedgerName = table.Column<string>(maxLength: 128, nullable: false),
                    GeneralLedgerDate = table.Column<DateTime>(nullable: false),
                    RefDocumentNo = table.Column<string>(maxLength: 24, nullable: false),
                    PostingDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralLedgerDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowStatus = table.Column<byte>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 24, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 24, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    GeneralLedgerId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AccountType = table.Column<string>(nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 24, nullable: false),
                    ExchangeRate = table.Column<decimal>(nullable: false),
                    ExchangeAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgerDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralLedgerDetails_GeneralLedgers_GeneralLedgerId",
                        column: x => x.GeneralLedgerId,
                        principalTable: "GeneralLedgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerDetails_GeneralLedgerId",
                table: "GeneralLedgerDetails",
                column: "GeneralLedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgers_GeneralLedgerNo",
                table: "GeneralLedgers",
                column: "GeneralLedgerNo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralLedgerDetails");

            migrationBuilder.DropTable(
                name: "GeneralLedgers");
        }
    }
}
