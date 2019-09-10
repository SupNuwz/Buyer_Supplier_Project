using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class changeWordAvailability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Availabiity",
                table: "Vehicle",
                newName: "Availability");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
   
            migrationBuilder.RenameColumn(
                name: "Availability",
                table: "Vehicle",
                newName: "Availabiity");
        }
    }
}
