using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Persistence.Migrations
{
    public partial class AdminPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Admins",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_PhotoId",
                table: "Admins",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Photos_PhotoId",
                table: "Admins",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Photos_PhotoId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_PhotoId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Admins");
        }
    }
}
