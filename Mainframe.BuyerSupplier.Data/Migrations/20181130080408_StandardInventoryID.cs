using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class StandardInventoryID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Configuration",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SupplierDefautInventoryItem",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "StandaradInventoryID",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandaradInventoryID",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Configuration",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierDefautInventoryItem",
                table: "Users",
                nullable: true);
        }
    }
}
