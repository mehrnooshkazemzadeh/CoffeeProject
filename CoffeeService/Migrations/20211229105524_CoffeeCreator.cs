using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeService.Migrations
{
    public partial class CoffeeCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Pdb");

            migrationBuilder.CreateTable(
                name: "CoffeeTypes",
                schema: "Pdb",
                columns: table => new
                {
                    CoffeeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    QuantityInPack = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeTypes", x => x.CoffeeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Coffees",
                schema: "Pdb",
                columns: table => new
                {
                    CoffeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoffeeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coffees", x => x.CoffeeId);
                    table.ForeignKey(
                        name: "FK_Coffees_CoffeeTypes_CoffeeTypeId",
                        column: x => x.CoffeeTypeId,
                        principalSchema: "Pdb",
                        principalTable: "CoffeeTypes",
                        principalColumn: "CoffeeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
             name: "CoffeeStores",
             schema: "Str",
             columns: table => new
             {
                 CoffeeStoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 CoffeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                 Poststatus = table.Column<int>(type: "int", nullable: false),
                 PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                 RecievedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                 Quantity = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_CoffeeStores", x => x.CoffeeStoreId);
             });

            migrationBuilder.CreateIndex(
                name: "IX_Coffees_CoffeeTypeId",
                schema: "Pdb",
                table: "Coffees",
                column: "CoffeeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coffees",
                schema: "Pdb");

            migrationBuilder.DropTable(
                name: "CoffeeTypes",
                schema: "Pdb");

            migrationBuilder.DropTable(
                name: "CoffeeStores",
                schema: "Str");
        }
    }
}
