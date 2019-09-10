using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class OrderRelatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "SupplierInventory");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "SupplierInventory",
                newName: "SupplierStandardInventoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "InventoryDate",
                table: "SupplierInventory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderRefNo",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInventory_SupplierStandardInventoryId",
                table: "SupplierInventory",
                column: "SupplierStandardInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierInventory_SupplierStandardInventory_SupplierStandardInventoryId",
                table: "SupplierInventory",
                column: "SupplierStandardInventoryId",
                principalTable: "SupplierStandardInventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierInventory_SupplierStandardInventory_SupplierStandardInventoryId",
                table: "SupplierInventory");

            migrationBuilder.DropIndex(
                name: "IX_SupplierInventory_SupplierStandardInventoryId",
                table: "SupplierInventory");

            migrationBuilder.DropColumn(
                name: "InventoryDate",
                table: "SupplierInventory");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderRefNo",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "SupplierStandardInventoryId",
                table: "SupplierInventory",
                newName: "UserID");

            migrationBuilder.AddColumn<int>(
                name: "ItemID",
                table: "SupplierInventory",
                nullable: false,
                defaultValue: 0);
        }
    }
}
