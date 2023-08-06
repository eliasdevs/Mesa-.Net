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
                    TipoJuego = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 14, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idReceptor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remitente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajes", x => x.Id);
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
                    IdRequest = table.Column<string>(type: "nvarchar(50)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "BlackJack_History",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdJugador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contadorMazo = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    Logger = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BlackjackId = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlackJack_History_Blackjacks_BlackjackId",
                        column: x => x.BlackjackId,
                        principalTable: "Blackjacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlackJack_Mazo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlackjackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_Mazo", x => new { x.BlackjackId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_Mazo_Blackjacks_BlackjackId",
                        column: x => x.BlackjackId,
                        principalTable: "Blackjacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManoJugadorVo",
                columns: table => new
                {
                    BlackjackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdJugador = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManoJugadorVo", x => new { x.BlackjackId, x.Id });
                    table.ForeignKey(
                        name: "FK_ManoJugadorVo_Blackjacks_BlackjackId",
                        column: x => x.BlackjackId,
                        principalTable: "Blackjacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlackJack_History_Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryBlackJackVoId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_History_Player", x => new { x.HistoryBlackJackVoId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_History_Player_BlackJack_History_HistoryBlackJackVoId",
                        column: x => x.HistoryBlackJackVoId,
                        principalTable: "BlackJack_History",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlackJack_Active_Hand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManoJugadorVoBlackjackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ManoJugadorVoId = table.Column<int>(type: "int", nullable: false),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_Active_Hand", x => new { x.ManoJugadorVoBlackjackId, x.ManoJugadorVoId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_Active_Hand_ManoJugadorVo_ManoJugadorVoBlackjackId_ManoJugadorVoId",
                        columns: x => new { x.ManoJugadorVoBlackjackId, x.ManoJugadorVoId },
                        principalTable: "ManoJugadorVo",
                        principalColumns: new[] { "BlackjackId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlackJack_History_BlackjackId",
                table: "BlackJack_History",
                column: "BlackjackId");

            migrationBuilder.CreateIndex(
                name: "IX_Blackjacks_IdRequest",
                table: "Blackjacks",
                column: "IdRequest",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackJack_Active_Hand");

            migrationBuilder.DropTable(
                name: "BlackJack_History_Player");

            migrationBuilder.DropTable(
                name: "BlackJack_Mazo");

            migrationBuilder.DropTable(
                name: "DeckOfCard_Cards");

            migrationBuilder.DropTable(
                name: "InfoJugador");

            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "ManoJugadorVo");

            migrationBuilder.DropTable(
                name: "BlackJack_History");

            migrationBuilder.DropTable(
                name: "DeckOfCard");

            migrationBuilder.DropTable(
                name: "Blackjacks");

            migrationBuilder.DropTable(
                name: "GameRequest");
        }
    }
}
