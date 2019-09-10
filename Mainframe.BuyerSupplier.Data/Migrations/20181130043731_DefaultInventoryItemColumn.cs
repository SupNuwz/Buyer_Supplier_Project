using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class DefaultInventoryItemColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupplierDefautInventoryItem",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierDefautInventoryItem",
                table: "Users");
        }
    }
}
