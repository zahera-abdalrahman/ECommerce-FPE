using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceFPE.Migrations
{
    /// <inheritdoc />
    public partial class addOneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.CreateTable(
                name: "ImagesProducts",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainImageUrl1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainImageUrl2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainImageUrl3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageOrder = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesProducts", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ImagesProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagesProducts_ProductId",
                table: "ImagesProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagesProducts");

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageOrder = table.Column<int>(type: "int", nullable: false),
                    MainImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainImageUrl1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainImageUrl2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainImageUrl3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }
    }
}
