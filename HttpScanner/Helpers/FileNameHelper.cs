namespace CodingTask.HttpScanner.Helpers;

public static class FileNameHelper
{
    public static string ToFileName(Uri url)
    {
        ArgumentNullException.ThrowIfNull(url);

        return url.ToString()
            .Replace("https://", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace("http://", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace(":", "_", StringComparison.Ordinal)
            .Replace("/", "_", StringComparison.Ordinal)
            .Replace("?", "_", StringComparison.Ordinal)
            .Replace("&", "_", StringComparison.Ordinal)
            .Replace("=", "_", StringComparison.Ordinal)
            .TrimEnd('_');
    }
}