using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class AddingExplicitLeaveTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_billingBusinessTravels_BusinessTravel_BusinessTravelId",
                table: "billingBusinessTravels");

            migrationBuilder.DropForeignKey(
                name: "FK_billingBusinessTravels_Currencies_CurrencyId",
                table: "billingBusinessTravels");

            migrationBuilder.DropForeignKey(
                name: "FK_billingBusinessTravels_TypeOfBillings_TypeOfBillingId",
                table: "billingBusinessTravels");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_LeaveTypes_LeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_billingBusinessTravels",
                table: "billingBusinessTravels");

            migrationBuilder.RenameTable(
                name: "billingBusinessTravels",
                newName: "BillingBusinessTravels");

            migrationBuilder.RenameIndex(
                name: "IX_billingBusinessTravels_TypeOfBillingId",
                table: "BillingBusinessTravels",
                newName: "IX_BillingBusinessTravels_TypeOfBillingId");

            migrationBuilder.RenameIndex(
                name: "IX_billingBusinessTravels_CurrencyId",
                table: "BillingBusinessTravels",
                newName: "IX_BillingBusinessTravels_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_billingBusinessTravels_BusinessTravelId",
                table: "BillingBusinessTravels",
                newName: "IX_BillingBusinessTravels_BusinessTravelId");

            migrationBuilder.AddColumn<int>(
                name: "ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillingBusinessTravels",
                table: "BillingBusinessTravels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CommonLeaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DefaultLimit = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonLeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExplicitLeaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Limit = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    OrganizationToken = table.Column<string>(nullable: true),
                    DepartmentToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExplicitLeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                column: "ExplicitLeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingBusinessTravels_BusinessTravel_BusinessTravelId",
                table: "BillingBusinessTravels",
                column: "BusinessTravelId",
                principalTable: "BusinessTravel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillingBusinessTravels_Currencies_CurrencyId",
                table: "BillingBusinessTravels",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillingBusinessTravels_TypeOfBillings_TypeOfBillingId",
                table: "BillingBusinessTravels",
                column: "TypeOfBillingId",
                principalTable: "TypeOfBillings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_ExplicitLeaveTypes_ExplicitLeaveTypeId",
                table: "LeaveAllocations",
                column: "ExplicitLeaveTypeId",
                principalTable: "ExplicitLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_CommonLeaveTypes_LeaveTypeId",
                table: "LeaveAllocations",
                column: "LeaveTypeId",
                principalTable: "CommonLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_CommonLeaveTypes_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId",
                principalTable: "CommonLeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingBusinessTravels_BusinessTravel_BusinessTravelId",
                table: "BillingBusinessTravels");

            migrationBuilder.DropForeignKey(
                name: "FK_BillingBusinessTravels_Currencies_CurrencyId",
                table: "BillingBusinessTravels");

            migrationBuilder.DropForeignKey(
                name: "FK_BillingBusinessTravels_TypeOfBillings_TypeOfBillingId",
                table: "BillingBusinessTravels");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_ExplicitLeaveTypes_ExplicitLeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_CommonLeaveTypes_LeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_CommonLeaveTypes_LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "CommonLeaveTypes");

            migrationBuilder.DropTable(
                name: "ExplicitLeaveTypes");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocations_ExplicitLeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillingBusinessTravels",
                table: "BillingBusinessTravels");

            migrationBuilder.DropColumn(
                name: "ExplicitLeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.RenameTable(
                name: "BillingBusinessTravels",
                newName: "billingBusinessTravels");

            migrationBuilder.RenameIndex(
                name: "IX_BillingBusinessTravels_TypeOfBillingId",
                table: "billingBusinessTravels",
                newName: "IX_billingBusinessTravels_TypeOfBillingId");

            migrationBuilder.RenameIndex(
                name: "IX_BillingBusinessTravels_CurrencyId",
                table: "billingBusinessTravels",
                newName: "IX_billingBusinessTravels_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_BillingBusinessTravels_BusinessTravelId",
                table: "billingBusinessTravels",
                newName: "IX_billingBusinessTravels_BusinessTravelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_billingBusinessTravels",
                table: "billingBusinessTravels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefaultLimit = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_billingBusinessTravels_BusinessTravel_BusinessTravelId",
                table: "billingBusinessTravels",
                column: "BusinessTravelId",
                principalTable: "BusinessTravel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_billingBusinessTravels_Currencies_CurrencyId",
                table: "billingBusinessTravels",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_billingBusinessTravels_TypeOfBillings_TypeOfBillingId",
                table: "billingBusinessTravels",
                column: "TypeOfBillingId",
                principalTable: "TypeOfBillings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_LeaveTypes_LeaveTypeId",
                table: "LeaveAllocations",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
