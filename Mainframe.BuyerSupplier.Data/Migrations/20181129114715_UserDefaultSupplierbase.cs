using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class UserDefaultSupplierbase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelevantZone",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "DefaultSupplierBaseId",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultSupplierBaseId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "RelevantZone",
                table: "Users",
                nullable: true);
        }
    }
}
