using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mainframe.BuyerSupplier.Data.Migrations
{
    public partial class SupplierStandardInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SupplierInventory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SupplierStandardInventory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StandardInventoryId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierStandardInventory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierStandardInventory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SupplierInventory");
        }
    }
}
