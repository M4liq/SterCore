using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class addedDateCreatedInBusinessTravel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessTravelVM");

            migrationBuilder.DropTable(
                name: "EmployeeVM");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "BusinessTravel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "BusinessTravel");

            migrationBuilder.CreateTable(
                name: "EmployeeVM",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessTravelVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DestinationCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PrepaymentAmount = table.Column<int>(type: "int", nullable: false),
                    PrepaymentCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurposeOfTravel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportVehicle = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_BusinessTravelVM_EmployeeId",
                table: "BusinessTravelVM",
                column: "EmployeeId");
        }
    }
}
