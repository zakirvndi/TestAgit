using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductionCar.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanningHeaders",
                columns: table => new
                {
                    PlanningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InputDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalActiveDays = table.Column<int>(type: "int", nullable: false),
                    TotalProduction = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningHeaders", x => x.PlanningID);
                });

            migrationBuilder.CreateTable(
                name: "PlanningDetails",
                columns: table => new
                {
                    DetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanningID = table.Column<int>(type: "int", nullable: false),
                    DayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayOrder = table.Column<int>(type: "int", nullable: false),
                    OriginalPlanning = table.Column<int>(type: "int", nullable: false),
                    DistributedPlanning = table.Column<int>(type: "int", nullable: false),
                    IsHoliday = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningDetails", x => x.DetailID);
                    table.ForeignKey(
                        name: "FK_PlanningDetails_PlanningHeaders",
                        column: x => x.PlanningID,
                        principalTable: "PlanningHeaders",
                        principalColumn: "PlanningID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanningDetails_PlanningID",
                table: "PlanningDetails",
                column: "PlanningID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanningDetails");

            migrationBuilder.DropTable(
                name: "PlanningHeaders");
        }
    }
}
