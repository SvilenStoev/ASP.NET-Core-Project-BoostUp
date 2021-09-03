using Microsoft.EntityFrameworkCore.Migrations;

namespace BoostUp.Data.Migrations
{
    public partial class AlterUsersRemoveUserId1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruiters_AspNetUsers_UserId1",
                table: "Recruiters");

            migrationBuilder.DropIndex(
                name: "IX_Recruiters_UserId1",
                table: "Recruiters");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Recruiters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Recruiters",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_UserId1",
                table: "Recruiters",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruiters_AspNetUsers_UserId1",
                table: "Recruiters",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
