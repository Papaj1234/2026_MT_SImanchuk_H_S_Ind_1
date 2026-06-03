using PerformanceAnalytics.Dtos;
using PerformanceAnalytics.Loading;

namespace PerformanceAnalytics.Interfaces;

public interface IMatrixAnalytics
{
    IReadOnlyList<TopMethodRow> TopMethodsFor2000(AnalyticsDataSet data);

    IReadOnlyList<ScalabilityRow> MatrixScalability(AnalyticsDataSet data);
}