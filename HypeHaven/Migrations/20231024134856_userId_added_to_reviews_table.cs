using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HypeHaven.Migrations
{
    /// <inheritdoc />
    public partial class userIdaddedtoreviewstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "reviews");
        }
    }
}
