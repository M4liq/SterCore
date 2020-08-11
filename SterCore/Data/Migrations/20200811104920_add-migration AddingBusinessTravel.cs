using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class addmigrationAddingBusinessTravel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessTravel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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

            migrationBuilder.CreateTable(
                name: "EmployeeVM",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    TaxId = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateJoined = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessTravelVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_BusinessTravelVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessTravelVM_EmployeeVM_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTravel_EmployeeId",
                table: "BusinessTravel",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTravelVM_EmployeeId",
                table: "BusinessTravelVM",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessTravel");

            migrationBuilder.DropTable(
                name: "BusinessTravelVM");

            migrationBuilder.DropTable(
                name: "EmployeeVM");
        }
    }
}
