using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Persistence.Migrations
{
    public partial class QrCodePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QrCodeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_QrCodeId",
                table: "Users",
                column: "QrCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Photos_QrCodeId",
                table: "Users",
                column: "QrCodeId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Photos_QrCodeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_QrCodeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "QrCodeId",
                table: "Users");
        }
    }
}
