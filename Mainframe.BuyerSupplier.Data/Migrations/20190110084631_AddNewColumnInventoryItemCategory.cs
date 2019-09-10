using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class AddNewColumnInventoryItemCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "StandardInventory");

            migrationBuilder.DropColumn(
                name: "SubGroup",
                table: "StandardInventory");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "OrderDetail",
                newName: "StandardInventoryId");

            migrationBuilder.AddColumn<int>(
                name: "StandardInventoryId",
                table: "SupplierInventory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InventoryItemCategoryId",
                table: "StandardInventory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InventoryItemSubCategoryId",
                table: "StandardInventory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandardInventoryId",
                table: "SupplierInventory");

            migrationBuilder.DropColumn(
                name: "InventoryItemCategoryId",
                table: "StandardInventory");

            migrationBuilder.DropColumn(
                name: "InventoryItemSubCategoryId",
                table: "StandardInventory");

            migrationBuilder.RenameColumn(
                name: "StandardInventoryId",
                table: "OrderDetail",
                newName: "ItemID");

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "StandardInventory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubGroup",
                table: "StandardInventory",
                nullable: true);
        }
    }
}
