using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseProduct_Categories_CategoryId",
                table: "BaseProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_BaseProduct_BaseProductId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseProduct",
                table: "BaseProduct");

            migrationBuilder.DropColumn(
                name: "BookInventory",
                table: "BaseProduct");

            migrationBuilder.RenameTable(
                name: "BaseProduct",
                newName: "Products");

            migrationBuilder.RenameColumn(
                name: "BaseProductId",
                table: "Images",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_BaseProductId",
                table: "Images",
                newName: "IX_Images_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseProduct_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Products",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<int>(
                name: "Inventory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "BaseProduct");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Images",
                newName: "BaseProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                newName: "IX_Images_BaseProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "BaseProduct",
                newName: "IX_BaseProduct_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "BaseProduct",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AddColumn<int>(
                name: "BookInventory",
                table: "BaseProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseProduct",
                table: "BaseProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProduct_Categories_CategoryId",
                table: "BaseProduct",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_BaseProduct_BaseProductId",
                table: "Images",
                column: "BaseProductId",
                principalTable: "BaseProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
