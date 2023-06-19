using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.Blackjack.Data.Migrations
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcceptedPlayerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdRequest = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "BlackJack_History",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMazo = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    Logger = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BackjackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlackJack_History_Blackjacks_BackjackId",
                        column: x => x.BackjackId,
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
                    BackjackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_Mazo", x => new { x.BackjackId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_Mazo_Blackjacks_BackjackId",
                        column: x => x.BackjackId,
                        principalTable: "Blackjacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerOneHand",
                columns: table => new
                {
                    BackjackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerOneHand", x => x.BackjackId);
                    table.ForeignKey(
                        name: "FK_PlayerOneHand_Blackjacks_BackjackId",
                        column: x => x.BackjackId,
                        principalTable: "Blackjacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTwoHand",
                columns: table => new
                {
                    BackjackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTwoHand", x => x.BackjackId);
                    table.ForeignKey(
                        name: "FK_PlayerTwoHand_Blackjacks_BackjackId",
                        column: x => x.BackjackId,
                        principalTable: "Blackjacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlackJack_History_PlayerOneHand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryBlackJackVoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_History_PlayerOneHand", x => new { x.HistoryBlackJackVoId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_History_PlayerOneHand_BlackJack_History_HistoryBlackJackVoId",
                        column: x => x.HistoryBlackJackVoId,
                        principalTable: "BlackJack_History",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlackJack_History_PlayerTwoHand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryBlackJackVoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_BlackJack_History_BackjackId",
                table: "BlackJack_History",
                column: "BackjackId");

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
                name: "BlackJack_History_PlayerOneHand");

            migrationBuilder.DropTable(
                name: "BlackJack_History_PlayerTwoHand");

            migrationBuilder.DropTable(
                name: "BlackJack_Mazo");

            migrationBuilder.DropTable(
                name: "DeckOfCard_Cards");

            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "PlayerOneHand");

            migrationBuilder.DropTable(
                name: "PlayerTwoHand");

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
