using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriBreak.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "DECIMAL(18,0)", precision: 18, scale: 0, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    WorkMode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BreakRecords",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "DECIMAL(18,0)", precision: 18, scale: 0, nullable: false),
                    UserId = table.Column<decimal>(type: "DECIMAL(18,0)", precision: 18, scale: 0, nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DurationMinutes = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Type = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Mood = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    EnergyLevel = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ScreenTimeMinutes = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "DECIMAL(18,0)", precision: 18, scale: 0, nullable: false),
                    UserId = table.Column<decimal>(type: "DECIMAL(18,0)", precision: 18, scale: 0, nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR2(120)", maxLength: 120, nullable: false),
                    Calories = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TimeOfDay = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreakRecords_UserId",
                table: "BreakRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserId",
                table: "Meals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakRecords");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
