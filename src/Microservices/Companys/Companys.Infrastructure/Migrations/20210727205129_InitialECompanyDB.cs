using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Companys.Infrastructure.Migrations
{
    public partial class InitialECompanyDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockExchange = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ceo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TurnOver = table.Column<decimal>(type: "money", maxLength: 15, precision: 19, scale: 2, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExchangeTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_ExchangeType_ExchangeTypeId",
                        column: x => x.ExchangeTypeId,
                        principalTable: "ExchangeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_Code",
                table: "Company",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_ExchangeTypeId",
                table: "Company",
                column: "ExchangeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "ExchangeType");
        }
    }
}
