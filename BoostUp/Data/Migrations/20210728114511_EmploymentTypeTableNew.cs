using Microsoft.EntityFrameworkCore.Migrations;

namespace BoostUp.Data.Migrations
{
    public partial class EmploymentTypeTableNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_EmploymentType_EmploymentTypeId",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmploymentType",
                table: "EmploymentType");

            migrationBuilder.RenameTable(
                name: "EmploymentType",
                newName: "EmploymentTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmploymentTypes",
                table: "EmploymentTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_EmploymentTypes_EmploymentTypeId",
                table: "Jobs",
                column: "EmploymentTypeId",
                principalTable: "EmploymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_EmploymentTypes_EmploymentTypeId",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmploymentTypes",
                table: "EmploymentTypes");

            migrationBuilder.RenameTable(
                name: "EmploymentTypes",
                newName: "EmploymentType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmploymentType",
                table: "EmploymentType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_EmploymentType_EmploymentTypeId",
                table: "Jobs",
                column: "EmploymentTypeId",
                principalTable: "EmploymentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
