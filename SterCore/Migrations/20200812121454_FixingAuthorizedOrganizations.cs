using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class FixingAuthorizedOrganizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizedOrganizations_Organization_OrganizationId",
                table: "AuthorizedOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_AuthorizedOrganizations_OrganizationId",
                table: "AuthorizedOrganizations");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AuthorizedOrganizations");

            migrationBuilder.AddColumn<int>(
                name: "AuthorizedOrganizationId",
                table: "Organization",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_AuthorizedOrganizationId",
                table: "Organization",
                column: "AuthorizedOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_AuthorizedOrganizations_AuthorizedOrganizationId",
                table: "Organization",
                column: "AuthorizedOrganizationId",
                principalTable: "AuthorizedOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_AuthorizedOrganizations_AuthorizedOrganizationId",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_AuthorizedOrganizationId",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "AuthorizedOrganizationId",
                table: "Organization");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AuthorizedOrganizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedOrganizations_OrganizationId",
                table: "AuthorizedOrganizations",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizedOrganizations_Organization_OrganizationId",
                table: "AuthorizedOrganizations",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
