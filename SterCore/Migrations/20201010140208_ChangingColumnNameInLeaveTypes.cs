using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class ChangingColumnNameInLeaveTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultDays",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "LeaveTypes");

            migrationBuilder.AddColumn<int>(
                name: "DefaultLimit",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultLimit",
                table: "LeaveTypes");

            migrationBuilder.AddColumn<int>(
                name: "DefaultDays",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
