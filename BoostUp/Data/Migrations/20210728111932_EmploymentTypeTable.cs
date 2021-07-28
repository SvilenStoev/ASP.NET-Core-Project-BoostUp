using Microsoft.EntityFrameworkCore.Migrations;

namespace BoostUp.Data.Migrations
{
    public partial class EmploymentTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmploymentType",
                table: "Jobs",
                newName: "EmploymentTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Categories",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "EmploymentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_EmploymentTypeId",
                table: "Jobs",
                column: "EmploymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_EmploymentType_EmploymentTypeId",
                table: "Jobs",
                column: "EmploymentTypeId",
                principalTable: "EmploymentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_EmploymentType_EmploymentTypeId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "EmploymentType");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_EmploymentTypeId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "EmploymentTypeId",
                table: "Jobs",
                newName: "EmploymentType");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);
        }
    }
}
