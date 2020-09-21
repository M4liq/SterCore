using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Migrations
{
    public partial class AddingAuthorizedDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorizedDepartmentId",
                table: "Department",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuthorizedDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorizedDepartmentToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedDepartments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_AuthorizedDepartmentId",
                table: "Department",
                column: "AuthorizedDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_AuthorizedDepartments_AuthorizedDepartmentId",
                table: "Department",
                column: "AuthorizedDepartmentId",
                principalTable: "AuthorizedDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_AuthorizedDepartments_AuthorizedDepartmentId",
                table: "Department");

            migrationBuilder.DropTable(
                name: "AuthorizedDepartments");

            migrationBuilder.DropIndex(
                name: "IX_Department_AuthorizedDepartmentId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "AuthorizedDepartmentId",
                table: "Department");
        }
    }
}
