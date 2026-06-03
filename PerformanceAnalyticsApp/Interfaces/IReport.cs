using PerformanceAnalytics.Loading;

namespace PerformanceAnalyticsApp.Interfaces;

public interface IReport
{
    string Title { get; }

    void Render(AnalyticsDataSet data);
}