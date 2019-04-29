using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_Project.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Symbol = table.Column<string>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IexId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Symbol);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStats",
                columns: table => new
                {
                    Symbol = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketCap = table.Column<double>(type: "float", nullable: false),
                    Revenue = table.Column<double>(type: "float", nullable: false),
                    GrossProfit = table.Column<double>(type: "float", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStats", x => x.Symbol);
                });

            migrationBuilder.CreateTable(
                name: "Divident",
                columns: table => new
                {
                    Exdate = table.Column<DateTime>(nullable: false),
                    Payment_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Record_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divident", x => x.Exdate);
                });

            migrationBuilder.CreateTable(
                name: "Logo",
                columns: table => new
                {
                    url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logo", x => x.url);
                });

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    Type = table.Column<string>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Performance = table.Column<double>(type: "float", nullable: false),
                    lastUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.Type);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyStats");

            migrationBuilder.DropTable(
                name: "Divident");

            migrationBuilder.DropTable(
                name: "Logo");

            migrationBuilder.DropTable(
                name: "Sector");
        }
    }
}
