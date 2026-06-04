using System.Globalization;
using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;
using PerformanceAnalyticsApp.Interfaces;
using Spectre.Console;

namespace PerformanceAnalyticsApp.Reports;

public sealed class ParallelismReport : IReport
{
    private readonly IScannerAnalytics analytics;

    public ParallelismReport(IScannerAnalytics analytics)
    {
        this.analytics = analytics;
    }

    public string Title => "Scanner parallelism saturation (1 / 10 / 50)";

    public void Render(AnalyticsDataSet data)
    {
        var rows = this.analytics.ScannerParallelism(data);

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Title("[bold]HTTP scanner: speedup by parallelism level[/]");

        table.AddColumn("Operation");
        table.AddColumn(new TableColumn("Threads").RightAligned());
        table.AddColumn(new TableColumn("Avg, ms").RightAligned());
        table.AddColumn(new TableColumn("Speedup vs 1").RightAligned());

        foreach (var r in rows)
        {
            var color = r.SpeedupVsSerial >= 5.0 ? "green" : r.SpeedupVsSerial >= 2.0 ? "yellow" : "grey";
            table.AddRow(
                r.Operation,
                r.MaxParallelism.ToString(CultureInfo.InvariantCulture),
                r.AvgElapsedMs.ToString("N1", CultureInfo.InvariantCulture),
                $"[{color}]{r.SpeedupVsSerial.ToString("N2", CultureInfo.InvariantCulture)}x[/]");
        }

        AnsiConsole.Write(table);

        AnsiConsole.WriteLine();

        var chart = new BarChart()
            .Width(60)
            .Label("[bold]Speedup vs single thread (x)[/]")
            .CenterLabel();

        foreach (var r in rows.Where(r => r.MaxParallelism > 1))
        {
            var label = $"{r.Operation} @ {r.MaxParallelism.ToString(CultureInfo.InvariantCulture)}";
            chart.AddItem(label, Math.Round(r.SpeedupVsSerial, 2), Color.Green);
        }

        AnsiConsole.Write(chart);
    }
}