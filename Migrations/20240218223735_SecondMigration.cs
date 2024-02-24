using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageRecongnitionMQTT.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MarkerValueBase64",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarkerValue",
                table: "Beams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkerValueBase64",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MarkerValue",
                table: "Beams");
        }
    }
}
