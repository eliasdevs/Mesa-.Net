using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.Blackjack.Data.Migrations
{
    /// <inheritdoc />
    public partial class cambioenMano : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackJack_Emp_hand");

            migrationBuilder.DropTable(
                name: "BlackJack_Ret_hand");

            migrationBuilder.DropColumn(
                name: "UserEmparejado_IdJugador",
                table: "Blackjacks");

            migrationBuilder.DropColumn(
                name: "UserRetador_IdJugador",
                table: "Blackjacks");

            migrationBuilder.CreateTable(
                name: "ManoJugadorVo",
                columns: table => new
                {
                    BlackjackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "BlackJack_Active_Hand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManoJugadorVoBlackjackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackJack_Active_Hand");

            migrationBuilder.DropTable(
                name: "ManoJugadorVo");

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
                    ManoJugadorVoBlackjackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_Emp_hand", x => new { x.ManoJugadorVoBlackjackId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_Emp_hand_Blackjacks_ManoJugadorVoBlackjackId",
                        column: x => x.ManoJugadorVoBlackjackId,
                        principalTable: "Blackjacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlackJack_Ret_hand",
                columns: table => new
                {
                    ManoJugadorVoBlackjackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalValue = table.Column<int>(type: "int", nullable: false),
                    Representation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubValue = table.Column<int>(type: "int", nullable: false),
                    TypeOfCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackJack_Ret_hand", x => new { x.ManoJugadorVoBlackjackId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlackJack_Ret_hand_Blackjacks_ManoJugadorVoBlackjackId",
                        column: x => x.ManoJugadorVoBlackjackId,
                        principalTable: "Blackjacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
