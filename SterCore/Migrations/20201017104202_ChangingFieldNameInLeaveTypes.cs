using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class ChangingFieldNameInLeaveTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultLimit",
                table: "CommonLeaveTypes");

            migrationBuilder.AddColumn<int>(
                name: "Limit",
                table: "CommonLeaveTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Limit",
                table: "CommonLeaveTypes");

            migrationBuilder.AddColumn<int>(
                name: "DefaultLimit",
                table: "CommonLeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
