using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHttpScanRun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HttpScanRuns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OperationType = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxParallelism = table.Column<int>(type: "INTEGER", nullable: false),
                    ElapsedMs = table.Column<long>(type: "INTEGER", nullable: false),
                    SuccessCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ErrorCount = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HostConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpScanRuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpScanRuns_HostConfigurations_HostConfigurationId",
                        column: x => x.HostConfigurationId,
                        principalTable: "HostConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HttpScanRuns_HostConfigurationId",
                table: "HttpScanRuns",
                column: "HostConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HttpScanRuns");
        }
    }
}
