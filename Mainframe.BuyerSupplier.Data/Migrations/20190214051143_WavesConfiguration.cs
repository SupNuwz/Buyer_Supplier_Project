using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class WavesConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandardInventoryId",
                table: "SupplierInventory");

            migrationBuilder.RenameColumn(
                name: "IsPreOrder",
                table: "Order",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<int>(
                name: "AssignmentSelectionType",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderType",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstWaveTime",
                table: "DeliverySlots",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondWaveTime",
                table: "DeliverySlots",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WatchList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StandardInventoryId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    DeliverySlotId = table.Column<int>(nullable: false),
                    SupplierInventoryId = table.Column<int>(nullable: false),
                    QOH = table.Column<decimal>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchList", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchList");

            migrationBuilder.DropColumn(
                name: "AssignmentSelectionType",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "FirstWaveTime",
                table: "DeliverySlots");

            migrationBuilder.DropColumn(
                name: "SecondWaveTime",
                table: "DeliverySlots");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Order",
                newName: "IsPreOrder");

            migrationBuilder.AddColumn<int>(
                name: "StandardInventoryId",
                table: "SupplierInventory",
                nullable: false,
                defaultValue: 0);
        }
    }
}
