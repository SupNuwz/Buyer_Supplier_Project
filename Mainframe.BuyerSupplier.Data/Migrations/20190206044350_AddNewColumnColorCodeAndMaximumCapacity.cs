using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class AddNewColumnColorCodeAndMaximumCapacity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "OrderDetail",
                newName: "StandardInventoryId");

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "Vehicle",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaximumCapacity",
                table: "Vehicle",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SupplierQuality",
                table: "Users",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "MaximumCapacity",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "SupplierQuality",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "StandardInventoryId",
                table: "OrderDetail",
                newName: "ItemID");

        }
    }
}
