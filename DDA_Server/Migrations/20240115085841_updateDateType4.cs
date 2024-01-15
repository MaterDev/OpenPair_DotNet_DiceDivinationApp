using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDA_Server.Migrations
{
    /// <inheritdoc />
    public partial class updateDateType4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LunarData",
                table: "DiceSpread",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LunarData",
                table: "DiceSpread");
        }
    }
}
