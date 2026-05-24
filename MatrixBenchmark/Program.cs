using System;
using System.IO;
using System.Threading.Tasks;
using CodingTask.Data;
using CodingTask.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ParallelGenericMatrix;

namespace MatrixBenchmark
{
    internal sealed class Program
    {
        static async Task Main(string[] args)
        {
            string matrixDir = args.Length > 0 ? args[0] : @"..\MatrixGenerator\bin\Debug\net8.0\matrices";
            string dbPath = args.Length > 1 ? args[1] : @"..\CodingTask.Data\codingTask.db";

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            using var dbContext = new AppDbContext(options);
            using var uow = new UnitOfWork(dbContext);

            var hostConfig = await new HostConfigurationService(uow).GetOrCreateAsync().ConfigureAwait(false);
            var runner = new BenchmarkRunner(uow);

            Console.WriteLine("- 10x10 -");
            var (a10r, b10r) = MatrixLoader.Load<RectMatrix<double>>(matrixDir, "matrix_10x10");
            await runner.RunAsync(hostConfig.Id, "RectMatrix", 10, a10r, b10r).ConfigureAwait(false);

            var (a10j, b10j) = MatrixLoader.Load<JaggedMatrix<double>>(matrixDir, "matrix_10x10");
            await runner.RunAsync(hostConfig.Id, "JaggedMatrix", 10, a10j, b10j).ConfigureAwait(false);

            var (a10f, b10f) = MatrixLoader.Load<FlatMatrix<double>>(matrixDir, "matrix_10x10");
            await runner.RunAsync(hostConfig.Id, "FlatMatrix", 10, a10f, b10f).ConfigureAwait(false);

            Console.WriteLine("- 500x500- ");
            var (a500r, b500r) = MatrixLoader.Load<RectMatrix<double>>(matrixDir, "matrix_500x500");
            await runner.RunAsync(hostConfig.Id, "RectMatrix", 500, a500r, b500r).ConfigureAwait(false);

            var (a500j, b500j) = MatrixLoader.Load<JaggedMatrix<double>>(matrixDir, "matrix_500x500");
            await runner.RunAsync(hostConfig.Id, "JaggedMatrix", 500, a500j, b500j).ConfigureAwait(false);

            var (a500f, b500f) = MatrixLoader.Load<FlatMatrix<double>>(matrixDir, "matrix_500x500");
            await runner.RunAsync(hostConfig.Id, "FlatMatrix", 500, a500f, b500f).ConfigureAwait(false);

            Console.WriteLine("- 2000x2000 -");
            var (a2000r, b2000r) = MatrixLoader.Load<RectMatrix<double>>(matrixDir, "matrix_2000x2000");
            await runner.RunAsync(hostConfig.Id, "RectMatrix", 2000, a2000r, b2000r).ConfigureAwait(false);

            var (a2000j, b2000j) = MatrixLoader.Load<JaggedMatrix<double>>(matrixDir, "matrix_2000x2000");
            await runner.RunAsync(hostConfig.Id, "JaggedMatrix", 2000, a2000j, b2000j).ConfigureAwait(false);

            var (a2000f, b2000f) = MatrixLoader.Load<FlatMatrix<double>>(matrixDir, "matrix_2000x2000");
            await runner.RunAsync(hostConfig.Id, "FlatMatrix", 2000, a2000f, b2000f).ConfigureAwait(false);

            BreakEvenAnalyzer.Analyze();
        }
    }
}