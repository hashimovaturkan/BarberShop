using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Persistence.Migrations
{
    public partial class updatePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Barbers");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Barbers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhotoId",
                table: "Users",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Barbers_PhotoId",
                table: "Barbers",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Barbers_Photos_PhotoId",
                table: "Barbers",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Photos_PhotoId",
                table: "Users",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barbers_Photos_PhotoId",
                table: "Barbers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Photos_PhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Barbers_PhotoId",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Barbers");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Barbers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
