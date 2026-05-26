namespace CodingTask.HttpScanner.Models;

public record ScanResult(
    Uri Url,
    int? StatusCode,
    long ContentLength,
    TimeSpan ResponseTime,
    string? Error = null);