using System;
using System.Threading.Tasks;
using CodingTask.Data.Entities;
using CodingTask.Data.Infrastructure;

namespace MatrixBenchmark
{
    internal sealed class BenchmarkSaver
    {
        private readonly IUnitOfWork uow;

        internal BenchmarkSaver(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        internal async Task SaveAsync(int hostConfigId, string matrixType, int size, string method, long elapsedMs)
        {
            await this.uow.MatrixBenchmarks.AddAsync(new MatrixBenchmarkResult
            {
                HostConfigurationId = hostConfigId,
                MatrixType = matrixType,
                MatrixSize = size,
                MethodName = method,
                ElapsedMs = elapsedMs,
                MeasuredAt = DateTime.UtcNow,
            }).ConfigureAwait(false);
            await this.uow.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}