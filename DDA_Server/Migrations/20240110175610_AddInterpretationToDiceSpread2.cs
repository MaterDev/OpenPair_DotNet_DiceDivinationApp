using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DDA_Server.Migrations
{
    /// <inheritdoc />
    public partial class AddInterpretationToDiceSpread2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiceSpread_ChatGPTResponse_InterpretationId",
                table: "DiceSpread");

            migrationBuilder.DropTable(
                name: "ChatGPTResponse");

            migrationBuilder.DropIndex(
                name: "IX_DiceSpread_InterpretationId",
                table: "DiceSpread");

            migrationBuilder.DropColumn(
                name: "InterpretationId",
                table: "DiceSpread");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.AddColumn<string>(
                name: "Interpretation",
                table: "DiceSpread",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interpretation",
                table: "DiceSpread");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.AddColumn<int>(
                name: "InterpretationId",
                table: "DiceSpread",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatGPTResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiceInterpretations = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false),
                    OverviewInterpretation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGPTResponse", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiceSpread_InterpretationId",
                table: "DiceSpread",
                column: "InterpretationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSpread_ChatGPTResponse_InterpretationId",
                table: "DiceSpread",
                column: "InterpretationId",
                principalTable: "ChatGPTResponse",
                principalColumn: "Id");
        }
    }
}
