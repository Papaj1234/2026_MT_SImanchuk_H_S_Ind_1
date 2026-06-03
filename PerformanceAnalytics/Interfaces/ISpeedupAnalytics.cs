using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Interfaces;

public interface ISpeedupAnalytics
{
    IReadOnlyList<SpeedupRow> Speedups(AnalyticsDataSet data);

    IReadOnlyList<SpeedupRow> Anomalies(AnalyticsDataSet data);
}