using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Analytics;

public sealed class ScannerAnalytics : IScannerAnalytics
{
    public IReadOnlyList<ParallelismRow> ScannerParallelism(AnalyticsDataSet data)
    {
        ArgumentNullException.ThrowIfNull(data);

        var grouped = data.ScanSamples
            .GroupBy(s => new { s.OperationType, s.MaxParallelism })
            .Select(g => new
            {
                g.Key.OperationType,
                g.Key.MaxParallelism,
                AvgMs = g.Average(s => s.ElapsedMs),
            })
            .ToList();

        var serialByOperation = grouped
            .Where(x => x.MaxParallelism == 1)
            .ToDictionary(x => x.OperationType, x => x.AvgMs);

        return grouped
            .GroupBy(x => x.OperationType)
            .SelectMany(opGroup => opGroup
                .OrderBy(x => x.MaxParallelism)
                .Select(x => new ParallelismRow(
                    x.OperationType.ToString(),
                    x.MaxParallelism,
                    x.AvgMs,
                    serialByOperation.TryGetValue(x.OperationType, out var serial) && x.AvgMs > 0
                        ? serial / x.AvgMs
                        : 1.0)))
            .ToList();
    }
}