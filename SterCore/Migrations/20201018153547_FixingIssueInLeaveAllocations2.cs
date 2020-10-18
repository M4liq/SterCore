using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class FixingIssueInLeaveAllocations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_CommonLeaveTypes_CommonTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocations_CommonTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "CommonTypeId",
                table: "LeaveAllocations");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_CommonLeaveTypeId",
                table: "LeaveAllocations",
                column: "CommonLeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_CommonLeaveTypes_CommonLeaveTypeId",
                table: "LeaveAllocations",
                column: "CommonLeaveTypeId",
                principalTable: "CommonLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_CommonLeaveTypes_CommonLeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocations_CommonLeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.AddColumn<int>(
                name: "CommonTypeId",
                table: "LeaveAllocations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_CommonTypeId",
                table: "LeaveAllocations",
                column: "CommonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_CommonLeaveTypes_CommonTypeId",
                table: "LeaveAllocations",
                column: "CommonTypeId",
                principalTable: "CommonLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
