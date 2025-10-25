using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingbookInvetorytothebooktable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookInventory",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookInventory",
                table: "Books");
        }
    }
}
