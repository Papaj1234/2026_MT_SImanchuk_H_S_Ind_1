using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Interfaces;
using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Analytics;

public sealed class SpeedupAnalytics : ISpeedupAnalytics
{
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

        var scanRows = data.ScanSamples
            .GroupBy(s => new { s.HostConfigurationId, s.OperationType })
            .Select(g => new
            {
                g.Key,
                Seq = g.Where(s => s.MaxParallelism == 1)
                       .Select(s => (double)s.ElapsedMs)
                       .DefaultIfEmpty()
                       .Average(),
                Par = g.Where(s => s.MaxParallelism > 1)
                       .Select(s => (double)s.ElapsedMs)
                       .DefaultIfEmpty()
                       .Average(),
            })
            .Where(x => x.Seq > 0 && x.Par > 0)
            .Select(x => new SpeedupRow(
                "Network",
                x.Key.OperationType.ToString(),
                0,
                x.Seq,
                x.Par,
                x.Seq / x.Par));

        return matrixRows.Concat(scanRows);
    }
} 