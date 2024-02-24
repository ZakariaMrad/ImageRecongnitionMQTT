using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageRecongnitionMQTT.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Href",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Href",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Href",
                table: "Esp32s",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Href",
                table: "Beams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Href",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Href",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Href",
                table: "Esp32s");

            migrationBuilder.DropColumn(
                name: "Href",
                table: "Beams");
        }
    }
}
