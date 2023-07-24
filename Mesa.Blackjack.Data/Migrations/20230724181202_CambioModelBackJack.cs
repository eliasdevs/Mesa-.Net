using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesa.Blackjack.Data.Migrations
{
    /// <inheritdoc />
    public partial class CambioModelBackJack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlackJack_History_Blackjacks_BackjackId",
                table: "BlackJack_History");

            migrationBuilder.DropForeignKey(
                name: "FK_BlackJack_Mazo_Blackjacks_BackjackId",
                table: "BlackJack_Mazo");

            migrationBuilder.DropTable(
                name: "PlayerOneHand");

            migrationBuilder.DropTable(
                name: "PlayerTwoHand");

            migrationBuilder.RenameColumn(
                name: "BackjackId",
                table: "BlackJack_Mazo",
                newName: "BlackjackId");

            migrationBuilder.RenameColumn(
                name: "BackjackId",
                table: "BlackJack_History",
                newName: "BlackjackId");

            migrationBuilder.RenameIndex(
                name: "IX_BlackJack_History_BackjackId",
                table: "BlackJack_History",
                newName: "IX_BlackJack_History_BlackjackId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BlackJack_History_Blackjacks_BlackjackId",
                table: "BlackJack_History",
                column: "BlackjackId",
                principalTable: "Blackjacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlackJack_Mazo_Blackjacks_BlackjackId",
                table: "BlackJack_Mazo",
                column: "BlackjackId",
                principalTable: "Blackjacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlackJack_History_Blackjacks_BlackjackId",
                table: "BlackJack_History");

            migrationBuilder.DropForeignKey(
                name: "FK_BlackJack_Mazo_Blackjacks_BlackjackId",
                table: "BlackJack_Mazo");

            migrationBuilder.DropColumn(
                name: "IdUserEmparejado",
                table: "Blackjacks");

            migrationBuilder.DropColumn(
                name: "IdUserRetador",
                table: "Blackjacks");

            migrationBuilder.RenameColumn(
                name: "BlackjackId",
                table: "BlackJack_Mazo",
                newName: "BackjackId");

            migrationBuilder.RenameColumn(
                name: "BlackjackId",
                table: "BlackJack_History",
                newName: "BackjackId");

            migrationBuilder.RenameIndex(
                name: "IX_BlackJack_History_BlackjackId",
                table: "BlackJack_History",
                newName: "IX_BlackJack_History_BackjackId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BlackJack_History_Blackjacks_BackjackId",
                table: "BlackJack_History",
                column: "BackjackId",
                principalTable: "Blackjacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlackJack_Mazo_Blackjacks_BackjackId",
                table: "BlackJack_Mazo",
                column: "BackjackId",
                principalTable: "Blackjacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
