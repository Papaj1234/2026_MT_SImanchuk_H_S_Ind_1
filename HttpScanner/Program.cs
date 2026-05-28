using CodingTask.Data;
using CodingTask.Data.Infrastructure;
using CodingTask.HttpScanner.Services;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.HttpScanner;

internal sealed class Program
{
    private const string DefaultUrlsFile = "Resources/urls.txt";
    private const string DefaultDownloadsDir = "downloads";

    private static readonly string DefaultDbPath = Path.Combine(
        AppContext.BaseDirectory,
        "..",
        "..",
        "..",
        "..",
        "CodingTask.Data",
        "codingTask.db");

    private static async Task Main(string[] args)
    {
        string urlsFile = args.Length > 0 ? args[0] : DefaultUrlsFile;
        string dbPath = args.Length > 1 ? args[1] : DefaultDbPath;
        string downloadsDir = args.Length > 2 ? args[2] : DefaultDownloadsDir;

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;

        using var dbContext = new AppDbContext(options);
        await dbContext.Database.MigrateAsync().ConfigureAwait(false);
        using var uow = new UnitOfWork(dbContext);
        using var scanner = new HttpScannerService();
        using var cts = new CancellationTokenSource();

        var metricsSaver = new MetricsSaver(uow);
        var orchestrator = new BenchmarkOrchestrator(scanner, metricsSaver);

        var urls = await File.ReadAllLinesAsync(urlsFile).ConfigureAwait(false);
        var urlList = urls.Where(u => !string.IsNullOrWhiteSpace(u)).ToList();

        await orchestrator.RunAnalyzeAsync(urlList, cts.Token).ConfigureAwait(false);
        await orchestrator.RunDownloadAsync(urlList, downloadsDir, cts.Token).ConfigureAwait(false);

        Console.WriteLine("\nAll done!");
    }
}