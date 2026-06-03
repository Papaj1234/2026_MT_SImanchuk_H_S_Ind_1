using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Interfaces;

public interface IEnvironmentAnalytics
{
    IReadOnlyList<EnvironmentRow> EnvironmentComparison(AnalyticsDataSet data);
}