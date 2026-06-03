using CodingTask.Data.Entities;

namespace PerformanceAnalytics.Loading;

public sealed record ScanSample(
    int HostConfigurationId,
    string HostLabel,
    string CpuLabel,
    OperationType OperationType,
    int MaxParallelism,
    long ElapsedMs,
    int SuccessCount,
    int ErrorCount);