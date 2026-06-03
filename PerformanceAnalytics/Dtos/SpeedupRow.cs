namespace PerformanceAnalytics.Dtos;

public sealed record SpeedupRow(
    string Scope,
    string Operation,
    int MatrixSize,
    double AvgSequentialMs,
    double AvgParallelMs,
    double Speedup);