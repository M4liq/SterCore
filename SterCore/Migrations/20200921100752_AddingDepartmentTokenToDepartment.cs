using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class AddingDepartmentTokenToDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentToken",
                table: "Department",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentToken",
                table: "Department");
        }
    }
}
