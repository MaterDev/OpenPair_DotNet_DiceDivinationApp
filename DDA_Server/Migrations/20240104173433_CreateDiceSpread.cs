using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DDA_Server.Migrations
{
    /// <inheritdoc />
    public partial class CreateDiceSpread : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiceSpread",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    D2 = table.Column<int>(type: "integer", nullable: false),
                    D4 = table.Column<int>(type: "integer", nullable: false),
                    D6 = table.Column<int>(type: "integer", nullable: false),
                    D8 = table.Column<int>(type: "integer", nullable: false),
                    D10_100 = table.Column<int>(type: "integer", nullable: false),
                    D12 = table.Column<int>(type: "integer", nullable: false),
                    D20 = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiceSpread", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiceSpread");
        }
    }
}
