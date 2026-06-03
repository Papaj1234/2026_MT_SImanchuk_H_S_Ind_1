using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;
using PerformanceAnalyticsApp.Interfaces;
using Spectre.Console;

namespace PerformanceAnalyticsApp.Reports;

public sealed class TopMethodsReport : IReport
{
    private readonly IMatrixAnalytics analytics;

    public TopMethodsReport(IMatrixAnalytics analytics)
    {
        this.analytics = analytics;
    }

    public string Title => "Top-3 methods for 2000x2000 matrices";

    public void Render(AnalyticsDataSet data)
    {
        var rows = this.analytics.TopMethodsFor2000(data);

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Title("[bold]Top-3 fastest methods (2000x2000)[/]");

        table.AddColumn("Structure");
        table.AddColumn("Rank");
        table.AddColumn("Method");
        table.AddColumn(new TableColumn("Avg, ms").RightAligned());

        foreach (var r in rows)
        {
            var color = r.Rank == 1 ? "green" : r.Rank == 2 ? "yellow" : "grey";
            table.AddRow(
                r.MatrixType,
                $"[{color}]{r.Rank}[/]",
                r.Method,
                $"[{color}]{r.AvgElapsedMs:N1}[/]");
        }

        AnsiConsole.Write(table);
    }
}