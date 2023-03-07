using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HypeHaven.Migrations
{
    /// <inheritdoc />
    public partial class CreateTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "brands");
        }
    }
}
