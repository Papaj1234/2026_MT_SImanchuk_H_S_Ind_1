namespace PerformanceAnalytics.Dtos;

public sealed record ScalabilityRow(
    string MatrixType,
    int MatrixSize,
    double AvgElapsedMs,
    double GrowthFactor);