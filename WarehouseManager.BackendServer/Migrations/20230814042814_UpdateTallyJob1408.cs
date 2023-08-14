using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManager.BackendServer.Migrations
{
    public partial class UpdateTallyJob1408 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PalletId",
                table: "TallyJobs",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PalletId",
                table: "TallyJobs");
        }
    }
}
