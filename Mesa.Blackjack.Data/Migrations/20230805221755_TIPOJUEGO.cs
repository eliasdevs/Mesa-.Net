using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.Blackjack.Data.Migrations
{
    /// <inheritdoc />
    public partial class TIPOJUEGO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoJuego",
                table: "GameRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoJuego",
                table: "GameRequest");
        }
    }
}
