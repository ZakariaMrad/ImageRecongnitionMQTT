using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageRecongnitionMQTT.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MarkerValueBase64",
                table: "Beams",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkerValueBase64",
                table: "Beams");
        }
    }
}
