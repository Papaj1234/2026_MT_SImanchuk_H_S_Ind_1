using System.Collections.Concurrent;

namespace CodingTask.HttpScanner.Helpers;

internal static class ParallelRunner
{
    internal static async Task ForEachAsync(
        IEnumerable<string> items,
        int maxParallelism,
        Func<string, Task> action,
        CancellationToken ct)
    {
        using var semaphore = new SemaphoreSlim(maxParallelism);

        var tasks = items.Select(async item =>
        {
            await semaphore.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                await action(item).ConfigureAwait(false);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    internal static async Task<IReadOnlyCollection<TResult>> ForEachAsync<TResult>(
        IEnumerable<string> items,
        int maxParallelism,
        Func<string, Task<TResult>> action,
        CancellationToken ct)
    {
        using var semaphore = new SemaphoreSlim(maxParallelism);
        var results = new ConcurrentBag<TResult>();

        var tasks = items.Select(async item =>
        {
            await semaphore.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                results.Add(await action(item).ConfigureAwait(false));
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks).ConfigureAwait(false);
        return results;
    }
}