using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        private static readonly string[] BuildStagesColumns = { "ProjectId", "StageTypeId", "StartedAt" };

        private static readonly string[] HostConfigurationsColumns = { "CpuModel", "PhysicalCoreCount", "LogicalThreadCount", "RamGb", "OperatingSystem" };

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HostConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CpuModel = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PhysicalCoreCount = table.Column<int>(type: "INTEGER", nullable: false),
                    LogicalThreadCount = table.Column<int>(type: "INTEGER", nullable: false),
                    RamGb = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    OperatingSystem = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueSeverities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueSeverities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    FolderPath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    StageTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DurationMs = table.Column<long>(type: "INTEGER", nullable: false),
                    ErrorCount = table.Column<int>(type: "INTEGER", nullable: false),
                    WarningCount = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildStages_BuildStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "BuildStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildStages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildStages_StageTypes_StageTypeId",
                        column: x => x.StageTypeId,
                        principalTable: "StageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BuildStageId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeverityId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoggedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueLogs_BuildStages_BuildStageId",
                        column: x => x.BuildStageId,
                        principalTable: "BuildStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueLogs_IssueSeverities_SeverityId",
                        column: x => x.SeverityId,
                        principalTable: "IssueSeverities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadSpeedMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BuildStageId = table.Column<int>(type: "INTEGER", nullable: false),
                    HostConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ParallelThreadCount = table.Column<int>(type: "INTEGER", nullable: false),
                    SequentialTimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    ParallelTimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadSpeedMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadSpeedMetrics_BuildStages_BuildStageId",
                        column: x => x.BuildStageId,
                        principalTable: "BuildStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThreadSpeedMetrics_HostConfigurations_HostConfigurationId",
                        column: x => x.HostConfigurationId,
                        principalTable: "HostConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildStages_ProjectId_StageTypeId_StartedAt",
                table: "BuildStages",
                columns: BuildStagesColumns,
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuildStages_StageTypeId",
                table: "BuildStages",
                column: "StageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildStages_StatusId",
                table: "BuildStages",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildStatuses_Name",
                table: "BuildStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostConfigurations_CpuModel_PhysicalCoreCount_LogicalThreadCount_RamGb_OperatingSystem",
                table: "HostConfigurations",
                columns: HostConfigurationsColumns,
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueLogs_BuildStageId",
                table: "IssueLogs",
                column: "BuildStageId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLogs_SeverityId",
                table: "IssueLogs",
                column: "SeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSeverities_Name",
                table: "IssueSeverities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FolderPath",
                table: "Projects",
                column: "FolderPath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StageTypes_Name",
                table: "StageTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadSpeedMetrics_BuildStageId",
                table: "ThreadSpeedMetrics",
                column: "BuildStageId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadSpeedMetrics_HostConfigurationId",
                table: "ThreadSpeedMetrics",
                column: "HostConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueLogs");

            migrationBuilder.DropTable(
                name: "ThreadSpeedMetrics");

            migrationBuilder.DropTable(
                name: "IssueSeverities");

            migrationBuilder.DropTable(
                name: "BuildStages");

            migrationBuilder.DropTable(
                name: "HostConfigurations");

            migrationBuilder.DropTable(
                name: "BuildStatuses");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "StageTypes");
        }
    }
}
