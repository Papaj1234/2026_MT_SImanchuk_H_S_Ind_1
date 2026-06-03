using CodingTask.Data.Entities;

namespace PerformanceAnalytics.Loading;

public sealed class AnalyticsDataSet
{
    public AnalyticsDataSet(
        IReadOnlyList<MatrixSample> matrixSamples,
        IReadOnlyList<ScanSample> scanSamples)
    {
        this.MatrixSamples = matrixSamples;
        this.ScanSamples = scanSamples;
    }

    public IReadOnlyList<MatrixSample> MatrixSamples { get; }

    public IReadOnlyList<ScanSample> ScanSamples { get; }
}