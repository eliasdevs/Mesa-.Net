using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.Blackjack.Data.Migrations
{
    /// <inheritdoc />
    public partial class cambio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlackJack_Emp_hand_Blackjacks_UserEmparejadoId",
                table: "BlackJack_Emp_hand");

            migrationBuilder.DropForeignKey(
                name: "FK_BlackJack_Ret_hand_Blackjacks_UserEmparejadoId",
                table: "BlackJack_Ret_hand");

            migrationBuilder.RenameColumn(
                name: "UserEmparejadoId",
                table: "BlackJack_Ret_hand",
                newName: "ManoJugadorVoBlackjackId");

            migrationBuilder.RenameColumn(
                name: "UserEmparejadoId",
                table: "BlackJack_Emp_hand",
                newName: "ManoJugadorVoBlackjackId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlackJack_Emp_hand_Blackjacks_ManoJugadorVoBlackjackId",
                table: "BlackJack_Emp_hand",
                column: "ManoJugadorVoBlackjackId",
                principalTable: "Blackjacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlackJack_Ret_hand_Blackjacks_ManoJugadorVoBlackjackId",
                table: "BlackJack_Ret_hand",
                column: "ManoJugadorVoBlackjackId",
                principalTable: "Blackjacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlackJack_Emp_hand_Blackjacks_ManoJugadorVoBlackjackId",
                table: "BlackJack_Emp_hand");

            migrationBuilder.DropForeignKey(
                name: "FK_BlackJack_Ret_hand_Blackjacks_ManoJugadorVoBlackjackId",
                table: "BlackJack_Ret_hand");

            migrationBuilder.RenameColumn(
                name: "ManoJugadorVoBlackjackId",
                table: "BlackJack_Ret_hand",
                newName: "UserEmparejadoId");

            migrationBuilder.RenameColumn(
                name: "ManoJugadorVoBlackjackId",
                table: "BlackJack_Emp_hand",
                newName: "UserEmparejadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlackJack_Emp_hand_Blackjacks_UserEmparejadoId",
                table: "BlackJack_Emp_hand",
                column: "UserEmparejadoId",
                principalTable: "Blackjacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlackJack_Ret_hand_Blackjacks_UserEmparejadoId",
                table: "BlackJack_Ret_hand",
                column: "UserEmparejadoId",
                principalTable: "Blackjacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
