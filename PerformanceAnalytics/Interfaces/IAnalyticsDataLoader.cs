using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Interfaces;

public interface IAnalyticsDataLoader
{
    Task<AnalyticsDataSet> LoadAsync();
}