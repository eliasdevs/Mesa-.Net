using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.Blackjack.Data.Migrations
{
    /// <inheritdoc />
    public partial class CambionEnHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackJack_History_PlayerTwoHand");

            migrationBuilder.AddColumn<string>(
                name: "IdJugador",
                table: "BlackJack_History",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdJugador",
                table: "BlackJack_History");

            migrationBuilder.CreateTable(
                name: "BlackJack_History_PlayerTwoHand",
                columns: table => new
                {
                    HistoryBlackJackVoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_History_PlayerTwoHand", x => new { x.HistoryBlackJackVoId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_History_PlayerTwoHand_BlackJack_History_HistoryBlackJackVoId",
                        column: x => x.HistoryBlackJackVoId,
                        principalTable: "BlackJack_History",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
