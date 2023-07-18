using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManager.BackendServer.Migrations
{
    public partial class UpdateReceipt1807 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CBM",
                table: "ReceiptOrders");

            migrationBuilder.DropColumn(
                name: "CustomDeclareNo",
                table: "ReceiptOrders");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ReceiptOrders");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "ReceiptDetails");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "ReceiptDetails");

            migrationBuilder.DropColumn(
                name: "Trouble",
                table: "ReceiptDetails");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "ReceiptDetails",
                newName: "Weight");

            migrationBuilder.AlterColumn<int>(
                name: "ReceivedQuantity",
                table: "ReceiptDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "ReceiptDetails",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "ReceiptDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "CustomDeclareNo",
                table: "ReceiptDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ReceiptDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ReceiptDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomDeclareNo",
                table: "ReceiptDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ReceiptDetails");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ReceiptDetails");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "ReceiptDetails",
                newName: "Width");

            migrationBuilder.AddColumn<decimal>(
                name: "CBM",
                table: "ReceiptOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CustomDeclareNo",
                table: "ReceiptOrders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "ReceiptOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "ReceivedQuantity",
                table: "ReceiptDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "ReceiptDetails",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "ReceiptDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "ReceiptDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "ReceiptDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Trouble",
                table: "ReceiptDetails",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "");
        }
    }
}
