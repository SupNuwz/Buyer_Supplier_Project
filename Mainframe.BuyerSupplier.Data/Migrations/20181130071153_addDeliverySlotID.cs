using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class addDeliverySlotID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliverySlot",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "DeliverySlotId",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliverySlotId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "DeliverySlot",
                table: "Users",
                nullable: true);
        }
    }
}
