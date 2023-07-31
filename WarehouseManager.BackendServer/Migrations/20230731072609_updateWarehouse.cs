using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManager.BackendServer.Migrations
{
    public partial class updateWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owned",
                table: "Warehouses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Owned",
                table: "Warehouses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
