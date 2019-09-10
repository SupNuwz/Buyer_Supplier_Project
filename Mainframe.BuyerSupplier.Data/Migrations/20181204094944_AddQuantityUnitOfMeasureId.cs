using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class AddQuantityUnitOfMeasureId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderQuantityBasis",
                table: "StandardInventory");

            migrationBuilder.AddColumn<int>(
                name: "QuantityUnitOfMesureId",
                table: "StandardInventory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityUnitOfMesureId",
                table: "StandardInventory");

            migrationBuilder.AddColumn<string>(
                name: "OrderQuantityBasis",
                table: "StandardInventory",
                nullable: true);
        }
    }
}
