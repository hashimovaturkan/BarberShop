using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Persistence.Migrations
{
    public partial class PhotoInFilial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Filials",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Filials_PhotoId",
                table: "Filials",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filials_Photos_PhotoId",
                table: "Filials",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filials_Photos_PhotoId",
                table: "Filials");

            migrationBuilder.DropIndex(
                name: "IX_Filials_PhotoId",
                table: "Filials");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Filials");
        }
    }
}
