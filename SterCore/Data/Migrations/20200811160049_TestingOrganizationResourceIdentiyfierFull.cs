using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class TestingOrganizationResourceIdentiyfierFull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "LeaveAllocations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "AspNetUsers");
        }
    }
}
