namespace PerformanceAnalytics.Dtos;

public sealed record TopMethodRow(
    string MatrixType,
    string Method,
    double AvgElapsedMs,
    int Rank);