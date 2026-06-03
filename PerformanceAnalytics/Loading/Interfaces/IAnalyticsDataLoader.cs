namespace PerformanceAnalytics.Loading.Interfaces;

public interface IAnalyticsDataLoader
{
    Task<AnalyticsDataSet> LoadAsync();
}