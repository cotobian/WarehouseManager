using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManager.BackendServer.Migrations
{
    public partial class updateWarehouse1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DoorNo",
                table: "WarehouseDoors",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DoorNo",
                table: "WarehouseDoors",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");
        }
    }
}
