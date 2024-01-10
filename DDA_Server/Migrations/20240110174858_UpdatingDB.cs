using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDA_Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiceSpread_ChatGPTResponse_interpretationId",
                table: "DiceSpread");

            migrationBuilder.RenameColumn(
                name: "interpretationId",
                table: "DiceSpread",
                newName: "InterpretationId");

            migrationBuilder.RenameIndex(
                name: "IX_DiceSpread_interpretationId",
                table: "DiceSpread",
                newName: "IX_DiceSpread_InterpretationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSpread_ChatGPTResponse_InterpretationId",
                table: "DiceSpread",
                column: "InterpretationId",
                principalTable: "ChatGPTResponse",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiceSpread_ChatGPTResponse_InterpretationId",
                table: "DiceSpread");

            migrationBuilder.RenameColumn(
                name: "InterpretationId",
                table: "DiceSpread",
                newName: "interpretationId");

            migrationBuilder.RenameIndex(
                name: "IX_DiceSpread_InterpretationId",
                table: "DiceSpread",
                newName: "IX_DiceSpread_interpretationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSpread_ChatGPTResponse_interpretationId",
                table: "DiceSpread",
                column: "interpretationId",
                principalTable: "ChatGPTResponse",
                principalColumn: "Id");
        }
    }
}
