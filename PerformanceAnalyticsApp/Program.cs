using CodingTask.Data;
using CodingTask.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PerformanceAnalytics.Analytics;
using PerformanceAnalytics.Loading;
using PerformanceAnalyticsApp.Interfaces;
using PerformanceAnalyticsApp.Menu;
using PerformanceAnalyticsApp.Reports;
using Spectre.Console;

namespace PerformanceAnalyticsApp;

internal static class Program
{
    private const string DatabaseFileName = "codingTask.db";

    private static async Task Main()
    {
        var dbPath = Path.Combine(AppContext.BaseDirectory, DatabaseFileName);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;

        using var context = new AppDbContext(options);
        await context.Database.MigrateAsync().ConfigureAwait(false);

        using var unitOfWork = new UnitOfWork(context);

        var loader = new AnalyticsDataLoader(unitOfWork);

        AnalyticsDataSet data = null!;
        await AnsiConsole.Status()
            .StartAsync("Loading performance data from database...", async _ =>
            {
                data = await loader.LoadAsync().ConfigureAwait(false);
            })
            .ConfigureAwait(false);

        var matrixAnalytics = new MatrixAnalytics();

        var speedupAnalytics = new SpeedupAnalytics();

        var reports = new List<IReport>
        {
            new TopMethodsReport(matrixAnalytics),
            new SpeedupReport(speedupAnalytics),
        };

        var menu = new ReportMenu(reports);
        menu.Run(data);
    }
}