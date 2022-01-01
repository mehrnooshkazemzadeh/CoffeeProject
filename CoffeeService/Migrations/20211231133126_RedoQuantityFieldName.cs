using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeService.Migrations
{
    public partial class RedoQuantityFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PackQuantity",
                schema: "Pdb",
                table: "Coffees",
                newName: "Quantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                schema: "Pdb",
                table: "Coffees",
                newName: "PackQuantity");
        }
    }
}
