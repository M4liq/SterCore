using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class TestingORI2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "MedicalCheckUps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "LeaveTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "LeaveRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "LeaveAllocations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "CompetenceTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "Competences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "BusinessTravel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "billingBusinessTravels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
