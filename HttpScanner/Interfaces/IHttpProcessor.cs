using CodingTask.HttpScanner.Models;

namespace CodingTask.HttpScanner.Interfaces;

public interface IHttpProcessor
{
    Task<IEnumerable<ScanResult>> AnalyzeAsync(
        IEnumerable<string> urls,
        int maxParallelism,
        CancellationToken ct);

    Task DownloadAsync(
        IEnumerable<string> urls,
        string destinationDir,
        int maxParallelism,
        CancellationToken ct);
}