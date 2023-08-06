using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.Blackjack.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManoJugador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackJack_History_PlayerOneHand");

            migrationBuilder.DropColumn(
                name: "IdUserEmparejado",
                table: "Blackjacks");

            migrationBuilder.DropColumn(
                name: "IdUserRetador",
                table: "Blackjacks");

            migrationBuilder.RenameColumn(
                name: "IdMazo",
                table: "BlackJack_History",
                newName: "contadorMazo");

            migrationBuilder.AddColumn<string>(
                name: "UserEmparejado_IdJugador",
                table: "Blackjacks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserRetador_IdJugador",
                table: "Blackjacks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BlackJack_Emp_hand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmparejadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_Emp_hand", x => new { x.UserEmparejadoId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_Emp_hand_Blackjacks_UserEmparejadoId",
                        column: x => x.UserEmparejadoId,
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
                    HistoryBlackJackVoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "BlackJack_Ret_hand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmparejadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_Ret_hand", x => new { x.UserEmparejadoId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_Ret_hand_Blackjacks_UserEmparejadoId",
                        column: x => x.UserEmparejadoId,
                        principalTable: "Blackjacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackJack_Emp_hand");

            migrationBuilder.DropTable(
                name: "BlackJack_History_Player");

            migrationBuilder.DropTable(
                name: "BlackJack_Ret_hand");

            migrationBuilder.DropColumn(
                name: "UserEmparejado_IdJugador",
                table: "Blackjacks");

            migrationBuilder.DropColumn(
                name: "UserRetador_IdJugador",
                table: "Blackjacks");

            migrationBuilder.RenameColumn(
                name: "contadorMazo",
                table: "BlackJack_History",
                newName: "IdMazo");

            migrationBuilder.AddColumn<string>(
                name: "IdUserEmparejado",
                table: "Blackjacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdUserRetador",
                table: "Blackjacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BlackJack_History_PlayerOneHand",
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
                    table.PrimaryKey("PK_BlackJack_History_PlayerOneHand", x => new { x.HistoryBlackJackVoId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_History_PlayerOneHand_BlackJack_History_HistoryBlackJackVoId",
                        column: x => x.HistoryBlackJackVoId,
                        principalTable: "BlackJack_History",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
