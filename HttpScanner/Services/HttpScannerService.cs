using CodingTask.HttpScanner.Interfaces;
using CodingTask.HttpScanner.Models;
using CodingTask.HttpScanner.Services;

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

    public Task<IEnumerable<ScanResult>> AnalyzeAsync(
        IEnumerable<string> urls,
        int maxParallelism,
        CancellationToken ct) =>
        this.analyzer.AnalyzeAsync(urls, maxParallelism, ct);

    public Task DownloadAsync(
        IEnumerable<string> urls,
        string destinationDir,
        int maxParallelism,
        CancellationToken ct) =>
        this.downloader.DownloadAsync(urls, destinationDir, maxParallelism, ct);

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