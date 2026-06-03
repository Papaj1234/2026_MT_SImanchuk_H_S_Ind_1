using PerformanceAnalytics.Loading;
using PerformanceAnalyticsApp.Interfaces;
using Spectre.Console;

namespace PerformanceAnalyticsApp.Menu;

public sealed class ReportMenu
{
    private const string ExitLabel = "Exit";

    private readonly IReadOnlyList<IReport> reports;

    public ReportMenu(IReadOnlyList<IReport> reports)
    {
        this.reports = reports;
    }

    public void Run(AnalyticsDataSet data)
    {
        ArgumentNullException.ThrowIfNull(data);

        while (true)
        {
            AnsiConsole.Clear();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Select a report:[/]")
                    .PageSize(10)
                    .WrapAround()
                    .AddChoices(this.reports.Select(r => r.Title).Append(ExitLabel)));

            if (choice == ExitLabel)
            {
                return;
            }

            var report = this.reports.First(r => r.Title == choice);

            AnsiConsole.Clear();
            report.Render(data);

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
            Console.ReadKey(true);
        }
    }
}