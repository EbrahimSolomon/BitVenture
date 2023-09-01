using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitVenture_EbrahimSolomon.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterRecords",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountHolder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterRecords", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DetailRecords",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveStatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeBreached = table.Column<bool>(type: "bit", nullable: false),
                    MasterRecordID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailRecords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DetailRecords_MasterRecords_MasterRecordID",
                        column: x => x.MasterRecordID,
                        principalTable: "MasterRecords",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailRecords_MasterRecordID",
                table: "DetailRecords",
                column: "MasterRecordID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailRecords");

            migrationBuilder.DropTable(
                name: "MasterRecords");
        }
    }
}
