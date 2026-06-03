using CodingTask.Data.Entities;
using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Analytics;

public sealed class AnalyticsQueries : IAnalyticsQueries
{
    public IReadOnlyList<TopMethodRow> TopMethodsFor2000(AnalyticsDataSet data)
    {
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

    public IReadOnlyList<SpeedupRow> Speedups(AnalyticsDataSet data)
    {
        return this.BuildSpeedups(data)
            .OrderBy(r => r.Scope)
            .ThenBy(r => r.Operation)
            .ThenBy(r => r.MatrixSize)
            .ToList();
    }

    public IReadOnlyList<SpeedupRow> Anomalies(AnalyticsDataSet data)
    {
        return this.BuildSpeedups(data)
            .Where(r => r.Speedup < 1.0)
            .OrderBy(r => r.Speedup)
            .ToList();
    }

    public IReadOnlyList<EnvironmentRow> EnvironmentComparison(AnalyticsDataSet data) =>
        throw new NotImplementedException();

    public IReadOnlyList<ScalabilityRow> MatrixScalability(AnalyticsDataSet data) =>
        throw new NotImplementedException();

    public IReadOnlyList<ParallelismRow> ScannerParallelism(AnalyticsDataSet data) =>
        throw new NotImplementedException();

    private IEnumerable<SpeedupRow> BuildSpeedups(AnalyticsDataSet data)
    {
        var matrixRows = data.MatrixSamples
            .GroupBy(s => new
            {
                s.HostConfigurationId,
                s.MatrixType,
                s.MatrixSize,
                BaseOp = MethodNaming.BaseOperation(s.Method),
            })
            .Select(g => new
            {
                g.Key,
                Seq = g.Where(s => !MethodNaming.IsParallel(s.Method))
                       .Select(s => (double)s.ElapsedMs)
                       .DefaultIfEmpty()
                       .Average(),
                Par = g.Where(s => MethodNaming.IsParallel(s.Method))
                       .Select(s => (double)s.ElapsedMs)
                       .DefaultIfEmpty()
                       .Average(),
            })
            .Where(x => x.Seq > 0 && x.Par > 0)
            .Select(x => new SpeedupRow(
                "Matrix",
                $"{x.Key.MatrixType} {x.Key.BaseOp}",
                x.Key.MatrixSize,
                x.Seq,
                x.Par,
                x.Seq / x.Par));

        return matrixRows;
    }
}