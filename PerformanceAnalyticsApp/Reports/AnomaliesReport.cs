using System.Globalization;
using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;
using PerformanceAnalyticsApp.Interfaces;
using Spectre.Console;

namespace PerformanceAnalyticsApp.Reports;

public sealed class AnomaliesReport : IReport
{
    private const double Threshold = 1.5;

    private readonly ISpeedupAnalytics analytics;

    public AnomaliesReport(ISpeedupAnalytics analytics)
    {
        this.analytics = analytics;
    }

    public string Title => "Parallelism anomalies";

    public void Render(AnalyticsDataSet data)
    {
        var anomalies = this.analytics.Anomalies(data);

        if (anomalies.Count > 0)
        {
            AnsiConsole.MarkupLine("[red]Anomalies found: parallel was slower than sequential.[/]");
            RenderChart(anomalies, "Anomalies (speedup < 1x)");
            return;
        }

        AnsiConsole.MarkupLine("[green]No anomalies found. Showing operations closest to overhead:[/]");

        var nearest = this.analytics.Speedups(data)
            .Where(r => r.Speedup < Threshold)
            .OrderBy(r => r.Speedup)
            .ToList();

        var thresholdText = Threshold.ToString("N1", CultureInfo.InvariantCulture);
        RenderChart(nearest, $"Closest to overhead (speedup < {thresholdText}x)");
    }

    private static void RenderChart(IReadOnlyList<SpeedupRow> rows, string label)
    {
        AnsiConsole.WriteLine();

        var chart = new BarChart()
            .Width(60)
            .Label($"[bold]{label}[/]")
            .CenterLabel();

        foreach (var r in rows)
        {
            var op = r.Operation.Replace("Matrix", string.Empty, StringComparison.Ordinal);
            var size = r.MatrixSize == 0 ? string.Empty : $" {r.MatrixSize.ToString(CultureInfo.InvariantCulture)}";
            var color = r.Speedup < 1.0 ? Color.Red : Color.Yellow;
            chart.AddItem($"{op}{size}", Math.Round(r.Speedup, 2), color);
        }

        AnsiConsole.Write(chart);
    }
}