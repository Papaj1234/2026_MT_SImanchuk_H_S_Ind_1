using System.Diagnostics;
using CodingTask.Data.Entities;
using CodingTask.HttpScanner.Interfaces;
using CodingTask.HttpScanner.Models;

namespace CodingTask.HttpScanner.Services;

internal sealed class BenchmarkOrchestrator
{
    private static readonly int[] ParallelismLevels = { 1, 10, 50 };
    private readonly IHttpProcessor scanner;
    private readonly MetricsSaver metricsSaver;

    internal BenchmarkOrchestrator(IHttpProcessor scanner, MetricsSaver metricsSaver)
    {
        this.scanner = scanner;
        this.metricsSaver = metricsSaver;
    }

    internal async Task RunAnalyzeAsync(
        IList<string> urls,
        CancellationToken ct)
    {
        Console.WriteLine("=== AnalyzeAsync ===");

        foreach (var parallelism in ParallelismLevels)
        {
            Console.WriteLine($"\n-- maxParallelism: {parallelism} --");
            var sw = Stopwatch.StartNew();

            var results = await this.scanner
                .AnalyzeAsync(urls, parallelism, ct)
                .ConfigureAwait(false);

            sw.Stop();

            var resultList = results.ToList();
            int success = resultList.Count(r => r.Error == null);
            int errors = resultList.Count(r => r.Error != null);

            Console.WriteLine($"Done in {sw.ElapsedMilliseconds} ms | Success: {success} | Errors: {errors}");

            await this.metricsSaver.SaveAsync(
                OperationType.Scan,
                parallelism,
                sw.ElapsedMilliseconds,
                success,
                errors,
                ct).ConfigureAwait(false);
        }
    }

    internal async Task RunDownloadAsync(
        IList<string> urls,
        string destinationDir,
        CancellationToken ct)
    {
        Console.WriteLine("\n=== DownloadAsync ===");

        foreach (var parallelism in ParallelismLevels)
        {
            Console.WriteLine($"\n-- maxParallelism: {parallelism} --");
            var sw = Stopwatch.StartNew();

            await this.scanner
                .DownloadAsync(urls, destinationDir, parallelism, ct)
                .ConfigureAwait(false);

            sw.Stop();
            Console.WriteLine($"Done in {sw.ElapsedMilliseconds} ms");

            await this.metricsSaver.SaveAsync(
                OperationType.Download,
                parallelism,
                sw.ElapsedMilliseconds,
                0,
                0,
                ct).ConfigureAwait(false);
        }
    }
}