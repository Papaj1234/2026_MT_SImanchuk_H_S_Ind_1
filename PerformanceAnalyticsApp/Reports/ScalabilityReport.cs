using System.Globalization;
using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;
using PerformanceAnalyticsApp.Interfaces;
using Spectre.Console;

namespace PerformanceAnalyticsApp.Reports;

public sealed class ScalabilityReport : IReport
{
    private readonly IMatrixAnalytics analytics;

    public ScalabilityReport(IMatrixAnalytics analytics)
    {
        this.analytics = analytics;
    }

    public string Title => "Matrix structure scalability (size growth)";

    public void Render(AnalyticsDataSet data)
    {
        var rows = this.analytics.MatrixScalability(data);

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Title("[bold]How execution time grows with matrix size[/]");

        table.AddColumn("Structure");
        table.AddColumn(new TableColumn("Size").RightAligned());
        table.AddColumn(new TableColumn("Avg, ms").RightAligned());
        table.AddColumn(new TableColumn("Growth vs prev").RightAligned());

        foreach (var r in rows)
        {
            var growth = r.GrowthFactor <= 1.0
                ? "-"
                : $"{r.GrowthFactor.ToString("N1", CultureInfo.InvariantCulture)}x";

            var color = r.GrowthFactor >= 50.0 ? "red" : r.GrowthFactor >= 20.0 ? "yellow" : "green";

            table.AddRow(
                r.MatrixType,
                r.MatrixSize.ToString(CultureInfo.InvariantCulture),
                r.AvgElapsedMs.ToString("N1", CultureInfo.InvariantCulture),
                r.GrowthFactor <= 1.0 ? "-" : $"[{color}]{growth}[/]");
        }

        AnsiConsole.Write(table);

        AnsiConsole.WriteLine();

        var chart = new BarChart()
            .Width(60)
            .Label("[bold]Avg time at 2000x2000 by structure (ms)[/]")
            .CenterLabel();

        foreach (var r in rows.Where(r => r.MatrixSize == 2000).OrderBy(r => r.AvgElapsedMs))
        {
            chart.AddItem(r.MatrixType, Math.Round(r.AvgElapsedMs, 1), Color.Aqua);
        }

        AnsiConsole.Write(chart);
    }
}