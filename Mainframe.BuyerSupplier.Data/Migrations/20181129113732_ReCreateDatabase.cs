using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class ReCreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderID = table.Column<int>(nullable: false),
                    VehicleNo = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    DeliveryTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ContactNo = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DeliverySlots",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SlotName = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    CutoffTime = table.Column<string>(nullable: true),
                    CountdownTime = table.Column<string>(nullable: true),
                    OrderAcceptTime = table.Column<string>(nullable: true),
                    OrderCofirmTime = table.Column<string>(nullable: true),
                    DispatchesConfirmTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverySlots", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItemCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItemSubCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InventoryItemCategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemSubCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderedDate = table.Column<DateTime>(nullable: false),
                    ExpectedDiliveredDate = table.Column<DateTime>(nullable: false),
                    IsPreOrder = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderAssignment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderID = table.Column<int>(nullable: false),
                    OrderDetailID = table.Column<int>(nullable: false),
                    SupplierInventoryID = table.Column<int>(nullable: false),
                    Qty = table.Column<decimal>(nullable: false),
                    SupplierAcknowledgement = table.Column<bool>(nullable: false),
                    VehicleAcknowledgement = table.Column<bool>(nullable: false),
                    BuyerAcknowledgement = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAssignment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    Qty = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StandardInventory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    OrderQuantityBasis = table.Column<string>(nullable: true),
                    Seasonality = table.Column<string>(nullable: true),
                    MinimumInventory = table.Column<decimal>(nullable: false),
                    SubGroup = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardInventory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SupplierBase",
                columns: table => new
                {
                    SupplierBaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupplierBaseName = table.Column<string>(nullable: true),
                    DeliverySlot = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierBase", x => x.SupplierBaseId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierInventory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Qty = table.Column<decimal>(nullable: false),
                    AvailableQty = table.Column<decimal>(nullable: false),
                    ProcessingQty = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierInventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasure",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasure", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ContactNo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserType = table.Column<string>(nullable: true),
                    RelevantZone = table.Column<string>(nullable: true),
                    DeliverySlot = table.Column<string>(nullable: true),
                    Configuration = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserSupplierBase",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    SupplierBaseID = table.Column<int>(nullable: false),
                    Distance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSupplierBase", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "DeliverySlots");

            migrationBuilder.DropTable(
                name: "InventoryItemCategory");

            migrationBuilder.DropTable(
                name: "InventoryItemSubCategory");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderAssignment");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "StandardInventory");

            migrationBuilder.DropTable(
                name: "SupplierBase");

            migrationBuilder.DropTable(
                name: "SupplierInventory");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserSupplierBase");
        }
    }
}
