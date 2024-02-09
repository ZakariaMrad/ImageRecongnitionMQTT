using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageRecongnitionMQTT.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beams",
                columns: table => new
                {
                    IdBeam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beams", x => x.IdBeam);
                });

            migrationBuilder.CreateTable(
                name: "ItemModel",
                columns: table => new
                {
                    IdItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeamModelIdBeam = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemModel", x => x.IdItem);
                    table.ForeignKey(
                        name: "FK_ItemModel_Beams_BeamModelIdBeam",
                        column: x => x.BeamModelIdBeam,
                        principalTable: "Beams",
                        principalColumn: "IdBeam");
                });

            migrationBuilder.CreateTable(
                name: "PositionModel",
                columns: table => new
                {
                    IdPosition = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    BeamModelIdBeam = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionModel", x => x.IdPosition);
                    table.ForeignKey(
                        name: "FK_PositionModel_Beams_BeamModelIdBeam",
                        column: x => x.BeamModelIdBeam,
                        principalTable: "Beams",
                        principalColumn: "IdBeam");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemModel_BeamModelIdBeam",
                table: "ItemModel",
                column: "BeamModelIdBeam");

            migrationBuilder.CreateIndex(
                name: "IX_PositionModel_BeamModelIdBeam",
                table: "PositionModel",
                column: "BeamModelIdBeam");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemModel");

            migrationBuilder.DropTable(
                name: "PositionModel");

            migrationBuilder.DropTable(
                name: "Beams");
        }
    }
}
