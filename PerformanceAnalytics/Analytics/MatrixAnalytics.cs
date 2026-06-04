using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Analytics;

public sealed class MatrixAnalytics : IMatrixAnalytics
{
    public IReadOnlyList<TopMethodRow> TopMethodsFor2000(AnalyticsDataSet data)
    {
        ArgumentNullException.ThrowIfNull(data);

        return data.MatrixSamples
            .Where(s => s.MatrixSize == 2000)
            .GroupBy(s => new { s.MatrixType, s.Method })
            .Select(g => new
            {
                g.Key.MatrixType,
                g.Key.Method,
                AvgMs = g.Average(s => s.ElapsedMs),
            })
            .GroupBy(x => x.MatrixType)
            .SelectMany(typeGroup => typeGroup
                .OrderBy(x => x.AvgMs)
                .Take(3)
                .Select((x, index) => new TopMethodRow(
                    x.MatrixType.ToString(),
                    x.Method.ToString(),
                    x.AvgMs,
                    index + 1)))
            .ToList();
    }

    public IReadOnlyList<ScalabilityRow> MatrixScalability(AnalyticsDataSet data)
    {
        ArgumentNullException.ThrowIfNull(data);

        return data.MatrixSamples
            .Where(s => !MethodNaming.IsParallel(s.Method) && s.MatrixSize >= 500)
            .GroupBy(s => new { s.MatrixType, s.MatrixSize })
            .Select(g => new
            {
                g.Key.MatrixType,
                g.Key.MatrixSize,
                AvgMs = g.Average(s => s.ElapsedMs),
            })
            .GroupBy(x => x.MatrixType)
            .SelectMany(typeGroup =>
            {
                var ordered = typeGroup.OrderBy(x => x.MatrixSize).ToList();
                return ordered.Select((x, i) => new ScalabilityRow(
                    x.MatrixType.ToString(),
                    x.MatrixSize,
                    x.AvgMs,
                    i == 0 ? 1.0 : x.AvgMs / ordered[i - 1].AvgMs));
            })
            .ToList();
    }
}