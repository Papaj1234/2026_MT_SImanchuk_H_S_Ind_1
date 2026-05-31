using System.Diagnostics;
using CodingTask.HttpScanner.Models;

namespace CodingTask.HttpScanner.Services;

internal sealed class HttpAnalyzer
{
    private readonly HttpClient client;

    internal HttpAnalyzer(HttpClient client)
    {
        this.client = client;
    }

    internal async Task<ScanResult> ScanUrlAsync(string url, CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            var uri = new Uri(url);
            using var response = await this.client
                .GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, ct)
                .ConfigureAwait(false);
            sw.Stop();
            return new ScanResult(
                url,
                (int)response.StatusCode,
                response.Content.Headers.ContentLength ?? 0,
                sw.Elapsed);
        }
        catch (HttpRequestException ex)
        {
            sw.Stop();
            return new ScanResult(url, null, 0, sw.Elapsed, ex.Message);
        }
        catch (TaskCanceledException ex)
        {
            sw.Stop();
            return new ScanResult(url, null, 0, sw.Elapsed, ex.Message);
        }
        catch (UriFormatException ex)
        {
            sw.Stop();
            return new ScanResult(url, null, 0, sw.Elapsed, ex.Message);
        }
    }
}