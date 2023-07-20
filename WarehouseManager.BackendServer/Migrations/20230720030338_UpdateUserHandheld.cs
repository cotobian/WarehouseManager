using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManager.BackendServer.Migrations
{
    public partial class UpdateUserHandheld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobType",
                table: "ForkliftJobs",
                newName: "jobType");

            migrationBuilder.AddColumn<int>(
                name: "handHeld",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "TallyJobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompletedUserId",
                table: "TallyJobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TallyJobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "TallyJobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TallyJobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiptDetailId",
                table: "TallyJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "jobStatus",
                table: "TallyJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "jobType",
                table: "TallyJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "handHeld",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "TallyJobs");

            migrationBuilder.DropColumn(
                name: "CompletedUserId",
                table: "TallyJobs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TallyJobs");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "TallyJobs");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TallyJobs");

            migrationBuilder.DropColumn(
                name: "ReceiptDetailId",
                table: "TallyJobs");

            migrationBuilder.DropColumn(
                name: "jobStatus",
                table: "TallyJobs");

            migrationBuilder.DropColumn(
                name: "jobType",
                table: "TallyJobs");

            migrationBuilder.RenameColumn(
                name: "jobType",
                table: "ForkliftJobs",
                newName: "JobType");
        }
    }
}
