namespace CodingTask.HttpScanner.Helpers;

internal static class ParallelRunner
{
    internal static async Task ForEachAsync(
        IEnumerable<string> urls,
        int maxParallelism,
        Func<string, Task> action,
        CancellationToken ct)
    {
        using var semaphore = new SemaphoreSlim(maxParallelism);

        var tasks = urls.Select(async url =>
        {
            await semaphore.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                await action(url).ConfigureAwait(false);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }
}