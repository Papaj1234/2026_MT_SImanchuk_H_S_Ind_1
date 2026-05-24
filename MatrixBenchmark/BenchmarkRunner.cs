using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CodingTask.Data.Infrastructure;
using ParallelGenericMatrix;

namespace MatrixBenchmark
{
    internal sealed class BenchmarkRunner
    {
        private readonly BenchmarkSaver saver;

        internal BenchmarkRunner(IUnitOfWork uow)
        {
            this.saver = new BenchmarkSaver(uow);
        }

        internal async Task RunAsync(int hostConfigId, string matrixType, int size, IMatrix<double> a, IMatrix<double> b)
        {
            await this.MeasureAsync(hostConfigId, matrixType, size, "AddByRowsSequential", () => a.AddByRowsSequential(b)).ConfigureAwait(false);
            await this.MeasureAsync(hostConfigId, matrixType, size, "AddByRowsParallel", () => a.AddByRowsParallel(b)).ConfigureAwait(false);
            await this.MeasureAsync(hostConfigId, matrixType, size, "AddByColumnsSequential", () => a.AddByColumnsSequential(b)).ConfigureAwait(false);
            await this.MeasureAsync(hostConfigId, matrixType, size, "AddByColumnsParallel", () => a.AddByColumnsParallel(b)).ConfigureAwait(false);
            await this.MeasureAsync(hostConfigId, matrixType, size, "MultiplySequential", () => a.MultiplySequential(b)).ConfigureAwait(false);
            await this.MeasureAsync(hostConfigId, matrixType, size, "MultiplyParallel", () => a.MultiplyParallel(b)).ConfigureAwait(false);
            await this.MeasureAsync(hostConfigId, matrixType, size, "MultiplyOptimSequential", () => a.MultiplyOptimSequential(b)).ConfigureAwait(false);
            await this.MeasureAsync(hostConfigId, matrixType, size, "MultiplyOptimParallel", () => a.MultiplyOptimParallel(b)).ConfigureAwait(false);
            await this.MeasureAsync(hostConfigId, matrixType, size, "MultiplyNaiveSequential", () => a.MultiplyNaiveSequential(b)).ConfigureAwait(false);
            await this.MeasureAsync(hostConfigId, matrixType, size, "MultiplyNaiveParallel", () => a.MultiplyNaiveParallel(b)).ConfigureAwait(false);
            Console.WriteLine();
        }

        private async Task MeasureAsync(int hostConfigId, string matrixType, int size, string method, Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            Console.WriteLine($"{matrixType} {size}x{size} | {method}: {sw.ElapsedMilliseconds} ms");
            await this.saver.SaveAsync(hostConfigId, matrixType, size, method, sw.ElapsedMilliseconds).ConfigureAwait(false);
        }
    }
}