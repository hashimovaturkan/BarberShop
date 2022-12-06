using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Persistence.Migrations
{
    public partial class UserBonuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBonuses",
                table: "Balances");

            migrationBuilder.AddColumn<int>(
                name: "UserBonuses",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Barbers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBonuses",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Barbers");

            migrationBuilder.AddColumn<int>(
                name: "UserBonuses",
                table: "Balances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
