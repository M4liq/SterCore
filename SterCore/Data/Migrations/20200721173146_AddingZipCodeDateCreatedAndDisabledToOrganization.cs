using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class AddingZipCodeDateCreatedAndDisabledToOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountingOffice",
                table: "Organization");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Organization",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "Organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Organization",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Organization");

            migrationBuilder.AddColumn<bool>(
                name: "AccountingOffice",
                table: "Organization",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
