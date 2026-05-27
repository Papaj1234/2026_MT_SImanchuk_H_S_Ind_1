namespace CodingTask.HttpScanner.Models;

public record ScanResult(
    string url,
    int? statusCode,
    long contentLength,
    TimeSpan responseTime,
    string? error = null);