using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Interfaces;

public interface IAnalyticsQueries
{
	IReadOnlyList<TopMethodRow> TopMethodsFor2000(AnalyticsDataSet data);

	IReadOnlyList<SpeedupRow> Speedups(AnalyticsDataSet data);

	IReadOnlyList<SpeedupRow> Anomalies(AnalyticsDataSet data);

	IReadOnlyList<EnvironmentRow> EnvironmentComparison(AnalyticsDataSet data);

	IReadOnlyList<ScalabilityRow> MatrixScalability(AnalyticsDataSet data);

	IReadOnlyList<ParallelismRow> ScannerParallelism(AnalyticsDataSet data);
}