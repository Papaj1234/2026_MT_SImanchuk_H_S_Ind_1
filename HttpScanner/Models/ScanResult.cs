namespace CodingTask.HttpScanner.Models;

public record ScanResult(
    string Url,
    int? StatusCode,
    long ContentLength,
    TimeSpan ResponseTime,
    string? Error = null);