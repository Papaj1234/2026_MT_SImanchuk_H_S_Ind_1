namespace PerformanceAnalytics.Dtos;

public sealed record ParallelismRow(
    string Operation,
    int MaxParallelism,
    double AvgElapsedMs,
    double SpeedupVsSerial);