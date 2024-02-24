using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageRecongnitionMQTT.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beams",
                columns: table => new
                {
                    IdBeam = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CanBeSaved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beams", x => x.IdBeam);
                });

            migrationBuilder.CreateTable(
                name: "Esp32s",
                columns: table => new
                {
                    IdEsp32 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MacAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Esp32s", x => x.IdEsp32);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    IdImage = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TakenBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AsBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.IdImage);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    IdItem = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MarkerValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.IdItem);
                });

            migrationBuilder.CreateTable(
                name: "BeamItems",
                columns: table => new
                {
                    IdBeamItem = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seenAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemModelIdItem = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeamItems", x => x.IdBeamItem);
                    table.ForeignKey(
                        name: "FK_BeamItems_Items_ItemModelIdItem",
                        column: x => x.ItemModelIdItem,
                        principalTable: "Items",
                        principalColumn: "IdItem");
                });

            migrationBuilder.CreateTable(
                name: "BeamModelItemModel",
                columns: table => new
                {
                    BeamsIdBeam = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemsIdItem = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeamModelItemModel", x => new { x.BeamsIdBeam, x.ItemsIdItem });
                    table.ForeignKey(
                        name: "FK_BeamModelItemModel_Beams_BeamsIdBeam",
                        column: x => x.BeamsIdBeam,
                        principalTable: "Beams",
                        principalColumn: "IdBeam",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeamModelItemModel_Items_ItemsIdItem",
                        column: x => x.ItemsIdItem,
                        principalTable: "Items",
                        principalColumn: "IdItem",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeamItems_ItemModelIdItem",
                table: "BeamItems",
                column: "ItemModelIdItem");

            migrationBuilder.CreateIndex(
                name: "IX_BeamModelItemModel_ItemsIdItem",
                table: "BeamModelItemModel",
                column: "ItemsIdItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeamItems");

            migrationBuilder.DropTable(
                name: "BeamModelItemModel");

            migrationBuilder.DropTable(
                name: "Esp32s");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Beams");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
