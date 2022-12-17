using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreEngine.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddedSequenceNumberToToots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserSequenceNumber",
                table: "MediaToots",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserSequenceNumber",
                table: "MediaToots");
        }
    }
}
