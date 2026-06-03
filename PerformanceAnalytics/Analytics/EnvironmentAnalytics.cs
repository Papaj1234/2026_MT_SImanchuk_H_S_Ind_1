using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Analytics;

public sealed class EnvironmentAnalytics : IEnvironmentAnalytics
{
    public IReadOnlyList<EnvironmentRow> EnvironmentComparison(AnalyticsDataSet data)
    {
        ArgumentNullException.ThrowIfNull(data);

        var matrixRows = data.MatrixSamples
                .GroupBy(s => new { s.CpuLabel, Operation = $"Matrix {s.MatrixType}" })
            .Select(g => new EnvironmentRow(
                g.Key.CpuLabel,
                g.Key.Operation,
                g.Average(s => s.ElapsedMs),
                g.Count()));

        var scanRows = data.ScanSamples
            .GroupBy(s => new { s.CpuLabel, Operation = $"Scan {s.OperationType}" })
            .Select(g => new EnvironmentRow(
                g.Key.CpuLabel,
                g.Key.Operation,
                g.Average(s => s.ElapsedMs),
                g.Count()));

        return matrixRows
            .Concat(scanRows)
            .OrderBy(r => r.Operation)
            .ThenBy(r => r.CpuLabel)
            .ToList();
    }
}