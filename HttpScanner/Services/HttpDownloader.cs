using CodingTask.HttpScanner.Helpers;

namespace CodingTask.HttpScanner.Services;

internal sealed class HttpDownloader
{
    private const long MaxFileSizeBytes = 10 * 1024 * 1024;
    private readonly HttpClient client;

    internal HttpDownloader(HttpClient client)
    {
        this.client = client;
    }

    internal async Task DownloadUrlAsync(string url, string destinationDir, CancellationToken ct)
    {
        try
        {
            var uri = new Uri(url);
            using var response = await this.client
                .GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, ct)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var contentLength = response.Content.Headers.ContentLength;
            if (contentLength.HasValue && contentLength.Value > MaxFileSizeBytes)
            {
                Console.WriteLine($"Skipped (too large): {url}");
                return;
            }

            var fileName = FileNameHelper.ToFileName(url);
            var filePath = Path.Combine(destinationDir, fileName);

            var fileStream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true);

            await using (fileStream.ConfigureAwait(false))
            {
                await response.Content.CopyToAsync(fileStream, ct).ConfigureAwait(false);
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Download error [{url}]: {ex.Message}");
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine($"Timeout [{url}]: {ex.Message}");
        }
        catch (UriFormatException ex)
        {
            Console.WriteLine($"Invalid URL [{url}]: {ex.Message}");
        }
    }
}