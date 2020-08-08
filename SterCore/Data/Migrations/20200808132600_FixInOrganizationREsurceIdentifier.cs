using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class FixInOrganizationREsurceIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_ApprovedById",
                table: "LeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_RequestingEmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveRequests",
                table: "LeaveRequests");

            migrationBuilder.RenameTable(
                name: "LeaveRequests",
                newName: "OrganizationResurceIdentifier");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequests_RequestingEmployeeId",
                table: "OrganizationResurceIdentifier",
                newName: "IX_OrganizationResurceIdentifier_RequestingEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "OrganizationResurceIdentifier",
                newName: "IX_OrganizationResurceIdentifier_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequests_ApprovedById",
                table: "OrganizationResurceIdentifier",
                newName: "IX_OrganizationResurceIdentifier_ApprovedById");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "OrganizationResurceIdentifier",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "LeaveTypeId",
                table: "OrganizationResurceIdentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "OrganizationResurceIdentifier",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRequested",
                table: "OrganizationResurceIdentifier",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateActioned",
                table: "OrganizationResurceIdentifier",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "OrganizationResurceIdentifier",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationResurceId",
                table: "OrganizationResurceIdentifier",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationResurceIdentifier",
                table: "OrganizationResurceIdentifier",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrganizationResurce",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationResurce", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationResurceIdentifier_OrganizationResurceId",
                table: "OrganizationResurceIdentifier",
                column: "OrganizationResurceId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationResurceIdentifier_AspNetUsers_ApprovedById",
                table: "OrganizationResurceIdentifier",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationResurceIdentifier_LeaveTypes_LeaveTypeId",
                table: "OrganizationResurceIdentifier",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationResurceIdentifier_AspNetUsers_RequestingEmployeeId",
                table: "OrganizationResurceIdentifier",
                column: "RequestingEmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationResurceIdentifier_OrganizationResurce_OrganizationResurceId",
                table: "OrganizationResurceIdentifier",
                column: "OrganizationResurceId",
                principalTable: "OrganizationResurce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationResurceIdentifier_AspNetUsers_ApprovedById",
                table: "OrganizationResurceIdentifier");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationResurceIdentifier_LeaveTypes_LeaveTypeId",
                table: "OrganizationResurceIdentifier");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationResurceIdentifier_AspNetUsers_RequestingEmployeeId",
                table: "OrganizationResurceIdentifier");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationResurceIdentifier_OrganizationResurce_OrganizationResurceId",
                table: "OrganizationResurceIdentifier");

            migrationBuilder.DropTable(
                name: "OrganizationResurce");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationResurceIdentifier",
                table: "OrganizationResurceIdentifier");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationResurceIdentifier_OrganizationResurceId",
                table: "OrganizationResurceIdentifier");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "OrganizationResurceIdentifier");

            migrationBuilder.DropColumn(
                name: "OrganizationResurceId",
                table: "OrganizationResurceIdentifier");

            migrationBuilder.RenameTable(
                name: "OrganizationResurceIdentifier",
                newName: "LeaveRequests");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationResurceIdentifier_RequestingEmployeeId",
                table: "LeaveRequests",
                newName: "IX_LeaveRequests_RequestingEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationResurceIdentifier_LeaveTypeId",
                table: "LeaveRequests",
                newName: "IX_LeaveRequests_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationResurceIdentifier_ApprovedById",
                table: "LeaveRequests",
                newName: "IX_LeaveRequests_ApprovedById");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "LeaveRequests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LeaveTypeId",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "LeaveRequests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRequested",
                table: "LeaveRequests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateActioned",
                table: "LeaveRequests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveRequests",
                table: "LeaveRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_ApprovedById",
                table: "LeaveRequests",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_RequestingEmployeeId",
                table: "LeaveRequests",
                column: "RequestingEmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
