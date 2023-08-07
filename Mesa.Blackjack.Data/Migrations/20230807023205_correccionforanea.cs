using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.BlackJack.Data.Migrations
{
    /// <inheritdoc />
    public partial class correccionforanea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlackJack_Active_Hand_ManoJugadorVo_ManoJugadorVoBlackjackId_ManoJugadorVoId",
                table: "BlackJack_Active_Hand");

            migrationBuilder.DropTable(
                name: "ManoJugadorVo");

            migrationBuilder.RenameColumn(
                name: "ManoJugadorVoId",
                table: "BlackJack_Active_Hand",
                newName: "ManoJugadorId");

            migrationBuilder.RenameColumn(
                name: "ManoJugadorVoBlackjackId",
                table: "BlackJack_Active_Hand",
                newName: "ManoJugadorBlackjackId");

            migrationBuilder.CreateTable(
                name: "ManoJugador",
                columns: table => new
                {
                    BlackjackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdJugador = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManoJugador", x => new { x.BlackjackId, x.Id });
                    table.ForeignKey(
                        name: "FK_ManoJugador_Blackjacks_BlackjackId",
                        column: x => x.BlackjackId,
                        principalTable: "Blackjacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BlackJack_Active_Hand_ManoJugador_ManoJugadorBlackjackId_ManoJugadorId",
                table: "BlackJack_Active_Hand",
                columns: new[] { "ManoJugadorBlackjackId", "ManoJugadorId" },
                principalTable: "ManoJugador",
                principalColumns: new[] { "BlackjackId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlackJack_Active_Hand_ManoJugador_ManoJugadorBlackjackId_ManoJugadorId",
                table: "BlackJack_Active_Hand");

            migrationBuilder.DropTable(
                name: "ManoJugador");

            migrationBuilder.RenameColumn(
                name: "ManoJugadorId",
                table: "BlackJack_Active_Hand",
                newName: "ManoJugadorVoId");

            migrationBuilder.RenameColumn(
                name: "ManoJugadorBlackjackId",
                table: "BlackJack_Active_Hand",
                newName: "ManoJugadorVoBlackjackId");

            migrationBuilder.CreateTable(
                name: "ManoJugadorVo",
                columns: table => new
                {
                    BlackjackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdJugador = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_BlackJack_Active_Hand_ManoJugadorVo_ManoJugadorVoBlackjackId_ManoJugadorVoId",
                table: "BlackJack_Active_Hand",
                columns: new[] { "ManoJugadorVoBlackjackId", "ManoJugadorVoId" },
                principalTable: "ManoJugadorVo",
                principalColumns: new[] { "BlackjackId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
