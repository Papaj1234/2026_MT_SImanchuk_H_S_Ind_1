using CodingTask.Data.Entities;

namespace PerformanceAnalytics.Loading;

public sealed record MatrixSample(
    int HostConfigurationId,
    string HostLabel,
    string CpuLabel,
    MatrixType MatrixType,
    int MatrixSize,
    MatrixMethod Method,
    long ElapsedMs);