using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AchieveBy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DailyTargetHours = table.Column<TimeSpan>(type: "time", nullable: false),
                    CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalId = table.Column<int>(type: "int", nullable: true),
                    CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartedAtDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAtDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GoalCodingSessionId = table.Column<int>(type: "int", nullable: true),
                    GoalTheorySessionId = table.Column<int>(type: "int", nullable: true),
                    ProjectCodingSessionId = table.Column<int>(type: "int", nullable: true),
                    ProjectTheorySessionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Goals_GoalCodingSessionId",
                        column: x => x.GoalCodingSessionId,
                        principalTable: "Goals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sessions_Goals_GoalTheorySessionId",
                        column: x => x.GoalTheorySessionId,
                        principalTable: "Goals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sessions_Projects_ProjectCodingSessionId",
                        column: x => x.ProjectCodingSessionId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sessions_Projects_ProjectTheorySessionId",
                        column: x => x.ProjectTheorySessionId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_GoalId",
                table: "Projects",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_GoalCodingSessionId",
                table: "Sessions",
                column: "GoalCodingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_GoalTheorySessionId",
                table: "Sessions",
                column: "GoalTheorySessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ProjectCodingSessionId",
                table: "Sessions",
                column: "ProjectCodingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ProjectTheorySessionId",
                table: "Sessions",
                column: "ProjectTheorySessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Goals");
        }
    }
}
