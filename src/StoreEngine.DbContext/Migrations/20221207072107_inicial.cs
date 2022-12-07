using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreEngine.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaToots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    TootId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HasAltText = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaToots", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaToots_AccountId_CreatedAt",
                table: "MediaToots",
                columns: new[] { "AccountId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_MediaToots_CreatedAt",
                table: "MediaToots",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_MediaToots_TootId",
                table: "MediaToots",
                column: "TootId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaToots");
        }
    }
}
