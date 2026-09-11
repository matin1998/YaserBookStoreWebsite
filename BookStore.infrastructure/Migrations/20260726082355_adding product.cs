using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "BaseProduct");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Images",
                newName: "BaseProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_BookId",
                table: "Images",
                newName: "IX_Images_BaseProductId");

            migrationBuilder.RenameColumn(
                name: "BookTitle",
                table: "BaseProduct",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "BookPrice",
                table: "BaseProduct",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "BookDescription",
                table: "BaseProduct",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryId",
                table: "BaseProduct",
                newName: "IX_BaseProduct_CategoryId");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                table: "BaseProduct",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "BookInventory",
                table: "BaseProduct",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseProduct",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Discriminator",
                table: "BaseProduct");

            migrationBuilder.RenameTable(
                name: "BaseProduct",
                newName: "Books");

            migrationBuilder.RenameColumn(
                name: "BaseProductId",
                table: "Images",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_BaseProductId",
                table: "Images",
                newName: "IX_Images_BookId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "BookTitle");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Books",
                newName: "BookPrice");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Books",
                newName: "BookDescription");

            migrationBuilder.RenameIndex(
                name: "IX_BaseProduct_CategoryId",
                table: "Books",
                newName: "IX_Books_CategoryId");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                table: "Books",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookInventory",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
