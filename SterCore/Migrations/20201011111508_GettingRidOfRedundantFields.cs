using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class GettingRidOfRedundantFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildCareLeaveLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EventLeaveLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RequestedLeaveLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SeekingJobLeaveLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SickLeaveLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UnpaidLeaveLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VacationLeaveLimit",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChildCareLeaveLimit",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventLeaveLimit",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestedLeaveLimit",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeekingJobLeaveLimit",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SickLeaveLimit",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnpaidLeaveLimit",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VacationLeaveLimit",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
