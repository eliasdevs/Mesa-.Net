using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.BlackJack.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlackJack_History",
                columns: table => new
                {
                    BlackJackId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdPlayer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContadorMazo = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    Logger = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_History", x => new { x.Id, x.BlackJackId });
                });

            migrationBuilder.CreateTable(
                name: "DeckOfCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 14, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckOfCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRequest",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcceptedPlayerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    GameMode = table.Column<int>(type: "int", nullable: false),
                    CreacionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MazoBlackJack",
                columns: table => new
                {
                    CardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlackJackId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdJugador = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    OriginalValue = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    SubValue = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MazoBlackJack", x => new { x.CardId, x.BlackJackId });
                });

            migrationBuilder.CreateTable(
                name: "DeckOfCard_Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeckOfCardsId = table.Column<int>(type: "int", nullable: false),
                    OriginalValue = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    SubValue = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckOfCard_Cards", x => new { x.DeckOfCardsId, x.Id });
                    table.ForeignKey(
                        name: "FK_DeckOfCard_Cards_DeckOfCard_DeckOfCardsId",
                        column: x => x.DeckOfCardsId,
                        principalTable: "DeckOfCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blackjacks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdRequest = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContadorMazo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blackjacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blackjacks_GameRequest_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "GameRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfoJugador",
                columns: table => new
                {
                    GameRequestBackJackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdContextWS = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoJugador", x => new { x.GameRequestBackJackId, x.Id });
                    table.ForeignKey(
                        name: "FK_InfoJugador_GameRequest_GameRequestBackJackId",
                        column: x => x.GameRequestBackJackId,
                        principalTable: "GameRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryBlackJack_BlackJackId",
                table: "BlackJack_History",
                column: "BlackJackId");

            migrationBuilder.CreateIndex(
                name: "IX_BlackJack_Id",
                table: "Blackjacks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Blackjacks_IdRequest",
                table: "Blackjacks",
                column: "IdRequest",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardBlackJack_BlackJackId",
                table: "MazoBlackJack",
                column: "BlackJackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackJack_History");

            migrationBuilder.DropTable(
                name: "Blackjacks");

            migrationBuilder.DropTable(
                name: "DeckOfCard_Cards");

            migrationBuilder.DropTable(
                name: "InfoJugador");

            migrationBuilder.DropTable(
                name: "MazoBlackJack");

            migrationBuilder.DropTable(
                name: "DeckOfCard");

            migrationBuilder.DropTable(
                name: "GameRequest");
        }
    }
}
