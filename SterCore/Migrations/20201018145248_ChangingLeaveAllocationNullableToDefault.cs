using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class ChangingLeaveAllocationNullableToDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_ExplicitLeaveTypes_ExplicitLeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationToken",
                table: "LeaveAllocations",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentToken",
                table: "LeaveAllocations",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CommonLeaveTypeId",
                table: "LeaveAllocations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_ExplicitLeaveTypes_ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                column: "ExplicitLeaveTypeId",
                principalTable: "ExplicitLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_ExplicitLeaveTypes_ExplicitLeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationToken",
                table: "LeaveAllocations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentToken",
                table: "LeaveAllocations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CommonLeaveTypeId",
                table: "LeaveAllocations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_ExplicitLeaveTypes_ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                column: "ExplicitLeaveTypeId",
                principalTable: "ExplicitLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
