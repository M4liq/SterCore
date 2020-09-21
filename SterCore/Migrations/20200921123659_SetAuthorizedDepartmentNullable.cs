using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class SetAuthorizedDepartmentNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_AuthorizedDepartments_AuthorizedDepartmentId",
                table: "Department");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorizedDepartmentId",
                table: "Department",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_AuthorizedDepartments_AuthorizedDepartmentId",
                table: "Department",
                column: "AuthorizedDepartmentId",
                principalTable: "AuthorizedDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_AuthorizedDepartments_AuthorizedDepartmentId",
                table: "Department");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorizedDepartmentId",
                table: "Department",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_AuthorizedDepartments_AuthorizedDepartmentId",
                table: "Department",
                column: "AuthorizedDepartmentId",
                principalTable: "AuthorizedDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
