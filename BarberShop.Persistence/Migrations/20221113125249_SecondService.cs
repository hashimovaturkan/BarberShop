using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Persistence.Migrations
{
    public partial class SecondService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Services_SecondServiceId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AdditionalServices_SecondServiceId",
                table: "Reservations",
                column: "SecondServiceId",
                principalTable: "AdditionalServices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AdditionalServices_SecondServiceId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Services_SecondServiceId",
                table: "Reservations",
                column: "SecondServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
