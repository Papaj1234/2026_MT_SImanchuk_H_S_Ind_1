using System.Globalization;
using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;
using PerformanceAnalyticsApp.Interfaces;
using Spectre.Console;

namespace PerformanceAnalyticsApp.Reports;

public sealed class EnvironmentReport : IReport
{
    private readonly IEnvironmentAnalytics analytics;

    public EnvironmentReport(IEnvironmentAnalytics analytics)
    {
        this.analytics = analytics;
    }

    public string Title => "Environment comparison (by CPU)";

    public void Render(AnalyticsDataSet data)
    {
        var rows = this.analytics.EnvironmentComparison(data);

        var distinctCpus = rows.Select(r => r.CpuLabel).Distinct().Count();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Title("[bold]Average execution time per CPU[/]");

        table.AddColumn("CPU");
        table.AddColumn("Operation");
        table.AddColumn(new TableColumn("Avg, ms").RightAligned());
        table.AddColumn(new TableColumn("Samples").RightAligned());

        foreach (var r in rows)
        {
            table.AddRow(
                r.CpuLabel,
                r.Operation,
                r.AvgElapsedMs.ToString("N1", CultureInfo.InvariantCulture),
                r.SampleCount.ToString(CultureInfo.InvariantCulture));
        }

        AnsiConsole.Write(table);

        AnsiConsole.WriteLine();

        if (distinctCpus <= 1)
        {
            AnsiConsole.MarkupLine(
                "[yellow]Note: all data was collected on a single CPU configuration. " +
                "Cross-environment comparison will populate automatically once runs from other machines are added, I guess[/]");
        }
    }
}