using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceFPE.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CartId",
                table: "Order",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Cart_CartId",
                table: "Order",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Cart_CartId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CartId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Order");
        }
    }
}
