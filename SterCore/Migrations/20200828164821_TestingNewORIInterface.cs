using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class TestingNewORIInterface : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "MedicalCheckUps");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "CompetenceTypes");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "Competences");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "BusinessTravel");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "billingBusinessTravels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "MedicalCheckUps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "LeaveAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "CompetenceTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "Competences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "BusinessTravel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "billingBusinessTravels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
