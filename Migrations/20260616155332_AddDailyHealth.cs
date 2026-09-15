using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thallo.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyHealth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyHealth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Steps = table.Column<int>(type: "INTEGER", nullable: true),
                    Calories = table.Column<int>(type: "INTEGER", nullable: true),
                    RestingHr = table.Column<int>(type: "INTEGER", nullable: true),
                    AvgHr = table.Column<int>(type: "INTEGER", nullable: true),
                    MinHr = table.Column<int>(type: "INTEGER", nullable: true),
                    MaxHr = table.Column<int>(type: "INTEGER", nullable: true),
                    AvgStress = table.Column<int>(type: "INTEGER", nullable: true),
                    AvgSpo2 = table.Column<int>(type: "INTEGER", nullable: true),
                    SleepTotal = table.Column<int>(type: "INTEGER", nullable: true),
                    SleepDeep = table.Column<int>(type: "INTEGER", nullable: true),
                    SleepLight = table.Column<int>(type: "INTEGER", nullable: true),
                    SleepRem = table.Column<int>(type: "INTEGER", nullable: true),
                    SleepAwake = table.Column<int>(type: "INTEGER", nullable: true),
                    SleepStart = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SleepEnd = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyHealth", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyHealth");
        }
    }
}
