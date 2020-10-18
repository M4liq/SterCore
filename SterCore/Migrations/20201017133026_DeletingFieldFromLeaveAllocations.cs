using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class DeletingFieldFromLeaveAllocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExplicit",
                table: "ExplicitLeaveTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExplicit",
                table: "ExplicitLeaveTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
