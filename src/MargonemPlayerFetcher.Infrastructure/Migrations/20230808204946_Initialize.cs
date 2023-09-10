using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MargoFetcher.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Hid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    St = table.Column<int>(type: "int", nullable: false),
                    Stat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tpl = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    LastFetchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FetchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CharId = table.Column<int>(type: "int", nullable: false),
                    Nick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Server = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    LastFetchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstFetchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayersLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    FetchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayersNick",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Nick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FetchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersNick", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerName" },
                values: new object[,]
                {
                    { 1, "classic" },
                    { 2, "aether" },
                    { 3, "aldous" },
                    { 4, "berufs" },
                    { 5, "brutal" },
                    { 6, "fobos" },
                    { 7, "gefion" },
                    { 8, "hutena" },
                    { 9, "jaruna" },
                    { 10, "katahha" },
                    { 11, "lelwani" },
                    { 12, "majuna" },
                    { 13, "nomada" },
                    { 14, "perkun" },
                    { 15, "tarhuna" },
                    { 16, "telawel" },
                    { 17, "tempest" },
                    { 18, "zemyna" },
                    { 19, "zorza" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_PlayerId_Hid",
                table: "Items",
                columns: new[] { "PlayerId", "Hid" });

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId_CharId_Server",
                table: "Players",
                columns: new[] { "UserId", "CharId", "Server" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayersLevels_PlayerId",
                table: "PlayersLevels",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersNick_PlayerId",
                table: "PlayersNick",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "PlayersLevels");

            migrationBuilder.DropTable(
                name: "PlayersNick");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
