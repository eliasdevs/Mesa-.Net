using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.Blackjack.Data.Migrations
{
    /// <inheritdoc />
    public partial class ContadorMazo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContadorMazo",
                table: "Blackjacks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContadorMazo",
                table: "Blackjacks");
        }
    }
}
