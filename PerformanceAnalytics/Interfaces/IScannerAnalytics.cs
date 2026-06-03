using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Interfaces;

public interface IScannerAnalytics
{
    IReadOnlyList<ParallelismRow> ScannerParallelism(AnalyticsDataSet data);
}