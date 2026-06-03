namespace PerformanceAnalytics.Loading;

public interface IAnalyticsDataLoader
{
    Task<AnalyticsDataSet> LoadAsync();
}