using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class AddingDepartmentTokenToAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "TrainingCourses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "ResourceTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "Resources",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "NotificationTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "MedicalCheckUps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "LeaveTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "LeaveRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "LeaveAllocations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "CompetenceTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "Competences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "BusinessTravel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "billingBusinessTravels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "Applications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "TrainingCourses");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "ResourceTypes");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "NotificationTypes");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "MedicalCheckUps");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "CompetenceTypes");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "Competences");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "BusinessTravel");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "billingBusinessTravels");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "Applications");
        }
    }
}
