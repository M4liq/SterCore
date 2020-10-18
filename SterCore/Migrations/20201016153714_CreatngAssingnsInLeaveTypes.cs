using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class CreatngAssingnsInLeaveTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AssignedMonthly",
                table: "ExplicitLeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AssignedYearly",
                table: "ExplicitLeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AssignedMonthly",
                table: "CommonLeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AssignedYearly",
                table: "CommonLeaveTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedMonthly",
                table: "ExplicitLeaveTypes");

            migrationBuilder.DropColumn(
                name: "AssignedYearly",
                table: "ExplicitLeaveTypes");

            migrationBuilder.DropColumn(
                name: "AssignedMonthly",
                table: "CommonLeaveTypes");

            migrationBuilder.DropColumn(
                name: "AssignedYearly",
                table: "CommonLeaveTypes");
        }
    }
}
