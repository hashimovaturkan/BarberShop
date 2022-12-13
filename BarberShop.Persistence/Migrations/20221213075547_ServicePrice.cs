using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Persistence.Migrations
{
    public partial class ServicePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Services",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "AdditionalServices",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "AdditionalServices");
        }
    }
}
