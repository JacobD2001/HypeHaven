using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HypeHaven.Migrations
{
    /// <inheritdoc />
    public partial class isFavorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "products");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "FavoriteProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "FavoriteProducts");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
