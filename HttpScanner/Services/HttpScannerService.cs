using System.Collections.Concurrent;
using CodingTask.HttpScanner.Helpers;
using CodingTask.HttpScanner.Interfaces;
using CodingTask.HttpScanner.Models;

namespace CodingTask.HttpScanner.Services;

public sealed class HttpScannerService : IHttpProcessor, IDisposable
{
    private readonly HttpClient apiClient;
    private readonly HttpClient downloadClient;
    private readonly HttpAnalyzer analyzer;
    private readonly HttpDownloader downloader;
    private bool disposed;

    public HttpScannerService()
    {
        this.apiClient = HttpClientFactory.CreateApiClient();
        this.downloadClient = HttpClientFactory.CreateDownloadClient();
        this.analyzer = new HttpAnalyzer(this.apiClient);
        this.downloader = new HttpDownloader(this.downloadClient);
    }

    public async Task<IEnumerable<ScanResult>> AnalyzeAsync(
        IEnumerable<string> urls,
        int maxParallelism,
        CancellationToken ct)
    {
        var results = new ConcurrentBag<ScanResult>();

        await ParallelRunner.ForEachAsync(
            urls,
            maxParallelism,
            async url =>
            {
                var result = await this.analyzer.ScanUrlAsync(url, ct).ConfigureAwait(false);
                results.Add(result);
            },
            ct).ConfigureAwait(false);

        return results;
    }

    public async Task DownloadAsync(
        IEnumerable<string> urls,
        string destinationDir,
        int maxParallelism,
        CancellationToken ct)
    {
        Directory.CreateDirectory(destinationDir);

        await ParallelRunner.ForEachAsync(
            urls,
            maxParallelism,
            url => this.downloader.DownloadUrlAsync(url, destinationDir, ct),
            ct).ConfigureAwait(false);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.apiClient.Dispose();
                this.downloadClient.Dispose();
            }

            this.disposed = true;
        }
    }
}