using System.Collections.Concurrent;
using System.Diagnostics;
using CodingTask.HttpScanner.Helpers;
using CodingTask.HttpScanner.Interfaces;
using CodingTask.HttpScanner.Models;

namespace CodingTask.HttpScanner.Services;

public sealed class HttpScannerService : IHttpProcessor, IDisposable
{
    private readonly HttpClient apiClient;
    private readonly HttpClient downloadClient;
    private bool disposed;

    private const long MaxFileSizeBytes = 10 * 1024 * 1024;

    public HttpScannerService()
    {
        this.apiClient = HttpClientFactory.CreateApiClient();
        this.downloadClient = HttpClientFactory.CreateDownloadClient();
    }

    public Task<IEnumerable<ScanResult>> AnalyzeAsync(
        IEnumerable<string> urls,
        int maxParallelism,
        CancellationToken ct) => throw new NotImplementedException();

    public Task DownloadAsync(
        IEnumerable<string> urls,
        string destinationDir,
        int maxParallelism,
        CancellationToken ct) => throw new NotImplementedException();

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