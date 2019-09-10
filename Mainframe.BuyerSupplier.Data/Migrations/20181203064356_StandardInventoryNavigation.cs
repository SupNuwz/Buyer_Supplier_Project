﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class StandardInventoryNavigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierStandardInventory_SupplierInventory_SupplierInventoryId",
                table: "SupplierStandardInventory");

            migrationBuilder.DropIndex(
                name: "IX_SupplierStandardInventory_SupplierInventoryId",
                table: "SupplierStandardInventory");

            migrationBuilder.DropColumn(
                name: "SupplierInventoryId",
                table: "SupplierStandardInventory");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierStandardInventory_StandardInventoryId",
                table: "SupplierStandardInventory",
                column: "StandardInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierStandardInventory_StandardInventory_StandardInventoryId",
                table: "SupplierStandardInventory",
                column: "StandardInventoryId",
                principalTable: "StandardInventory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierStandardInventory_StandardInventory_StandardInventoryId",
                table: "SupplierStandardInventory");

            migrationBuilder.DropIndex(
                name: "IX_SupplierStandardInventory_StandardInventoryId",
                table: "SupplierStandardInventory");

            migrationBuilder.AddColumn<int>(
                name: "SupplierInventoryId",
                table: "SupplierStandardInventory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierStandardInventory_SupplierInventoryId",
                table: "SupplierStandardInventory",
                column: "SupplierInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierStandardInventory_SupplierInventory_SupplierInventoryId",
                table: "SupplierStandardInventory",
                column: "SupplierInventoryId",
                principalTable: "SupplierInventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
