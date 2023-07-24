using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.Blackjack.Data.Migrations
{
    /// <inheritdoc />
    public partial class StatusGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Blackjacks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Blackjacks");
        }
    }
}
