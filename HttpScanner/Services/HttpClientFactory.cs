namespace CodingTask.HttpScanner.Services;

public static class HttpClientFactory
{
    public static HttpClient CreateApiClient()
    {
        return new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10),
        };
    }

    public static HttpClient CreateDownloadClient()
    {
        return new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(5),
        };
    }
}