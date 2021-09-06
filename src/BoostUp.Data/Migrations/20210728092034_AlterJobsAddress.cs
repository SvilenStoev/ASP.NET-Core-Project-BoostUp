using Microsoft.EntityFrameworkCore.Migrations;

namespace BoostUp.Data.Migrations
{
    public partial class AlterJobsAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_AddressId",
                table: "Jobs",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Addresses_AddressId",
                table: "Jobs",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Addresses_AddressId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_AddressId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Jobs");
        }
    }
}
