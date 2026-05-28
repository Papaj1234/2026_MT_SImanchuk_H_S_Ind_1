using CodingTask.Common.Services;
using CodingTask.Data.Entities;
using CodingTask.Data.Infrastructure;

namespace CodingTask.HttpScanner.Services;

internal sealed class MetricsSaver
{
    private readonly IUnitOfWork uow;

    internal MetricsSaver(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    internal async Task SaveAsync(
        OperationType operationType,
        int maxParallelism,
        long elapsedMs,
        int successCount,
        int errorCount,
        CancellationToken ct)
    {
        var hostConfig = await new HostConfigurationService(this.uow)
            .GetOrCreateAsync()
            .ConfigureAwait(false);

        var run = new HttpScanRun
        {
            OperationType = operationType,
            MaxParallelism = maxParallelism,
            ElapsedMs = elapsedMs,
            SuccessCount = successCount,
            ErrorCount = errorCount,
            MeasuredAt = DateTime.UtcNow,
            HostConfigurationId = hostConfig.Id,
        };

        await this.uow.HttpScanRuns.AddAsync(run).ConfigureAwait(false);
        await this.uow.SaveChangesAsync().ConfigureAwait(false);
    }
}