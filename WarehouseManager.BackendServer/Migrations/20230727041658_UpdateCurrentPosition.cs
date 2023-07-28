using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManager.BackendServer.Migrations
{
    public partial class UpdateCurrentPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CurrentPositions");

            migrationBuilder.RenameColumn(
                name: "ReceiptDetailId",
                table: "CurrentPositions",
                newName: "PalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PalletId",
                table: "CurrentPositions",
                newName: "ReceiptDetailId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CurrentPositions",
                type: "int",
                nullable: true);
        }
    }
}
