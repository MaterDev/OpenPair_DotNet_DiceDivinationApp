using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDA_Server.Migrations
{
    /// <inheritdoc />
    public partial class updateDateType5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dalle3ImageUrl",
                table: "DiceSpread",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dalle3ImageUrl",
                table: "DiceSpread");
        }
    }
}
