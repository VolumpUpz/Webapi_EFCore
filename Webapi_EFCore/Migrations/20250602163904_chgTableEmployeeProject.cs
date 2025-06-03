using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webapi_EFCore.Migrations
{
    /// <inheritdoc />
    public partial class chgTableEmployeeProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploteeProject_Employees_EmployeeId",
                table: "EmploteeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmploteeProject_Project_ProjectId",
                table: "EmploteeProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmploteeProject",
                table: "EmploteeProject");

            migrationBuilder.RenameTable(
                name: "EmploteeProject",
                newName: "EmployeeProject");

            migrationBuilder.RenameIndex(
                name: "IX_EmploteeProject_ProjectId",
                table: "EmployeeProject",
                newName: "IX_EmployeeProject_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProject",
                table: "EmployeeProject",
                columns: new[] { "EmployeeId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employees_EmployeeId",
                table: "EmployeeProject",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Project_ProjectId",
                table: "EmployeeProject",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employees_EmployeeId",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Project_ProjectId",
                table: "EmployeeProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProject",
                table: "EmployeeProject");

            migrationBuilder.RenameTable(
                name: "EmployeeProject",
                newName: "EmploteeProject");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProject_ProjectId",
                table: "EmploteeProject",
                newName: "IX_EmploteeProject_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmploteeProject",
                table: "EmploteeProject",
                columns: new[] { "EmployeeId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmploteeProject_Employees_EmployeeId",
                table: "EmploteeProject",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmploteeProject_Project_ProjectId",
                table: "EmploteeProject",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
