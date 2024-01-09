using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DDA_Server.Migrations
{
    /// <inheritdoc />
    public partial class addDiceInterpreations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.AddColumn<int>(
                name: "interpretationId",
                table: "DiceSpread",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatGPTResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OverviewInterpretation = table.Column<string>(type: "text", nullable: true),
                    DiceInterpretations = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGPTResponse", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiceSpread_interpretationId",
                table: "DiceSpread",
                column: "interpretationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSpread_ChatGPTResponse_interpretationId",
                table: "DiceSpread",
                column: "interpretationId",
                principalTable: "ChatGPTResponse",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiceSpread_ChatGPTResponse_interpretationId",
                table: "DiceSpread");

            migrationBuilder.DropTable(
                name: "ChatGPTResponse");

            migrationBuilder.DropIndex(
                name: "IX_DiceSpread_interpretationId",
                table: "DiceSpread");

            migrationBuilder.DropColumn(
                name: "interpretationId",
                table: "DiceSpread");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:hstore", ",,");
        }
    }
}
