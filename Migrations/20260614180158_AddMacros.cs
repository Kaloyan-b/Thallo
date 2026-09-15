using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thallo.Migrations
{
    /// <inheritdoc />
    public partial class AddMacros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Calories",
                table: "Logs",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Protein",
                table: "Logs",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Salt",
                table: "Logs",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Sugar",
                table: "Logs",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Sugar",
                table: "Logs");
        }
    }
}
