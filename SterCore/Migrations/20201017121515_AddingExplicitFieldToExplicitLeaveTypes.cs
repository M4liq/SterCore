using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class AddingExplicitFieldToExplicitLeaveTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_CommonLeaveTypes_LeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.AlterColumn<int>(
                name: "LeaveTypeId",
                table: "LeaveAllocations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CommonLeaveTypeId",
                table: "LeaveAllocations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsExplicit",
                table: "ExplicitLeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_CommonLeaveTypes_LeaveTypeId",
                table: "LeaveAllocations",
                column: "LeaveTypeId",
                principalTable: "CommonLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_CommonLeaveTypes_LeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "CommonLeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "IsExplicit",
                table: "ExplicitLeaveTypes");

            migrationBuilder.AlterColumn<int>(
                name: "LeaveTypeId",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_CommonLeaveTypes_LeaveTypeId",
                table: "LeaveAllocations",
                column: "LeaveTypeId",
                principalTable: "CommonLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
