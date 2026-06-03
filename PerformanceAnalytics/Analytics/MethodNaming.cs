using CodingTask.Data.Entities;

namespace PerformanceAnalytics.Analytics;

public static string BaseOperation(MatrixMethod method)
{
    var name = method.ToString();

    const string parallel = "Parallel";
    const string sequential = "Sequential";

    if (name.EndsWith(parallel, StringComparison.Ordinal))
    {
        return name.Substring(0, name.Length - parallel.Length);
    }

    if (name.EndsWith(sequential, StringComparison.Ordinal))
    {
        return name.Substring(0, name.Length - sequential.Length);
    }

    return name;
}