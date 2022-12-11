using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Persistence.Migrations
{
    public partial class BarberFilial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilialId",
                table: "Barbers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Barbers_FilialId",
                table: "Barbers",
                column: "FilialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Barbers_Filials_FilialId",
                table: "Barbers",
                column: "FilialId",
                principalTable: "Filials",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barbers_Filials_FilialId",
                table: "Barbers");

            migrationBuilder.DropIndex(
                name: "IX_Barbers_FilialId",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "FilialId",
                table: "Barbers");
        }
    }
}
