namespace PerformanceAnalytics.Dtos;

public sealed record EnvironmentRow(
    string CpuLabel,
    string Operation,
    double AvgElapsedMs,
    int SampleCount);