using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class CreatingNullableForeignKeysInLeaveAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_ExplicitLeaveTypes_ExplicitLeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.AlterColumn<int>(
                name: "ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CommonLeaveTypeId",
                table: "LeaveAllocations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_ExplicitLeaveTypes_ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                column: "ExplicitLeaveTypeId",
                principalTable: "ExplicitLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_ExplicitLeaveTypes_ExplicitLeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.AlterColumn<int>(
                name: "ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CommonLeaveTypeId",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_ExplicitLeaveTypes_ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                column: "ExplicitLeaveTypeId",
                principalTable: "ExplicitLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
