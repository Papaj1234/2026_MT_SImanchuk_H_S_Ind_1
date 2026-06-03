using CodingTask.Data.Entities;

namespace PerformanceAnalytics.Analytics;

public static class MethodNaming
{
    public static bool IsParallel(MatrixMethod method) =>
        method.ToString().EndsWith("Parallel", StringComparison.Ordinal);

    public static string BaseOperation(MatrixMethod method)
    {
        var name = method.ToString();

        if (name.EndsWith("Parallel", StringComparison.Ordinal))
        {
            return name[..^"Parallel".Length];
        }

        if (name.EndsWith("Sequential", StringComparison.Ordinal))
        {
            return name[..^"Sequential".Length];
        }

        return name;
    }
}