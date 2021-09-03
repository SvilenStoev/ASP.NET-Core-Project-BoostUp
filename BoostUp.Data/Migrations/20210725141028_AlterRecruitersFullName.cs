using Microsoft.EntityFrameworkCore.Migrations;

namespace BoostUp.Data.Migrations
{
    public partial class AlterRecruitersFullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Recruiters",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Recruiters",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Recruiters");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Recruiters",
                newName: "FullName");
        }
    }
}
