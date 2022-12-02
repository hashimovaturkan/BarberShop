using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Persistence.Migrations
{
    public partial class OrderandBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance_Users_UserId",
                table: "Balance");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AdditionalServices_AdditionalServiceId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Services_ServiceId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_UserId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Balance",
                table: "Balance");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Balance",
                newName: "Balances");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ServiceId",
                table: "Orders",
                newName: "IX_Orders_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AdditionalServiceId",
                table: "Orders",
                newName: "IX_Orders_AdditionalServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Balance_UserId",
                table: "Balances",
                newName: "IX_Balances_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Balances",
                table: "Balances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Users_UserId",
                table: "Balances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AdditionalServices_AdditionalServiceId",
                table: "Orders",
                column: "AdditionalServiceId",
                principalTable: "AdditionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Services_ServiceId",
                table: "Orders",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Users_UserId",
                table: "Balances");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AdditionalServices_AdditionalServiceId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Services_ServiceId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Balances",
                table: "Balances");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Balances",
                newName: "Balance");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ServiceId",
                table: "Order",
                newName: "IX_Order_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AdditionalServiceId",
                table: "Order",
                newName: "IX_Order_AdditionalServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Balances_UserId",
                table: "Balance",
                newName: "IX_Balance_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Balance",
                table: "Balance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_Users_UserId",
                table: "Balance",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AdditionalServices_AdditionalServiceId",
                table: "Order",
                column: "AdditionalServiceId",
                principalTable: "AdditionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Services_ServiceId",
                table: "Order",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
