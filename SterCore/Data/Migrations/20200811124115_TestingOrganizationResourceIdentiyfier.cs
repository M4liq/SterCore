using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class TestingOrganizationResourceIdentiyfier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "Organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "LeaveTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationToken",
                table: "LeaveRequests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessTravel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationToken = table.Column<string>(nullable: true),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    DestinationCountry = table.Column<string>(nullable: true),
                    DestinationCity = table.Column<string>(nullable: true),
                    PurposeOfTravel = table.Column<string>(nullable: true),
                    TransportVehicle = table.Column<string>(nullable: true),
                    AdditionalInfo = table.Column<string>(nullable: true),
                    PrepaymentAmount = table.Column<int>(nullable: false),
                    PrepaymentCurrency = table.Column<string>(nullable: true),
                    ApplicationId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTravel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessTravel_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTravel_EmployeeId",
                table: "BusinessTravel",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessTravel");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "OrganizationToken",
                table: "LeaveRequests");
        }
    }
}
