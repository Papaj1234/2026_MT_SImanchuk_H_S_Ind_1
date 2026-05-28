using CodingTask.Data;
using CodingTask.Data.Entities;
using CodingTask.Data.Infrastructure;
using CodingTask.Common.Services;
using Microsoft.EntityFrameworkCore;
using ParallelGenericMatrix;

namespace MatrixBenchmark;

internal sealed class Program
{
    private const string DefaultMatrixDir = @"..\MatrixGenerator\bin\Debug\net8.0\matrices";
    private const string DefaultDbPath = @"..\CodingTask.Data\codingTask.db";

    private static async Task Main(string[] args)
    {
        string matrixDir = args.Length > 0 ? args[0] : DefaultMatrixDir;
        string dbPath = args.Length > 1 ? args[1] : DefaultDbPath;

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;

        using var dbContext = new AppDbContext(options);
        using var uow = new UnitOfWork(dbContext);

        var hostConfig = await new HostConfigurationService(uow).GetOrCreateAsync().ConfigureAwait(false);
        var runner = new BenchmarkRunner(uow);

        Console.WriteLine("- 10x10 -");
        await runner.RunAsync(hostConfig.Id, MatrixType.RectMatrix, 10,
            RectMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_10x10_A.bin")),
            RectMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_10x10_B.bin"))).ConfigureAwait(false);
        await runner.RunAsync(hostConfig.Id, MatrixType.JaggedMatrix, 10,
            JaggedMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_10x10_A.bin")),
            JaggedMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_10x10_B.bin"))).ConfigureAwait(false);
        await runner.RunAsync(hostConfig.Id, MatrixType.FlatMatrix, 10,
            FlatMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_10x10_A.bin")),
            FlatMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_10x10_B.bin"))).ConfigureAwait(false);

        Console.WriteLine("- 500x500 -");
        await runner.RunAsync(hostConfig.Id, MatrixType.RectMatrix, 500,
            RectMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_500x500_A.bin")),
            RectMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_500x500_B.bin"))).ConfigureAwait(false);
        await runner.RunAsync(hostConfig.Id, MatrixType.JaggedMatrix, 500,
            JaggedMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_500x500_A.bin")),
            JaggedMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_500x500_B.bin"))).ConfigureAwait(false);
        await runner.RunAsync(hostConfig.Id, MatrixType.FlatMatrix, 500,
            FlatMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_500x500_A.bin")),
            FlatMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_500x500_B.bin"))).ConfigureAwait(false);

        Console.WriteLine("- 2000x2000 -");
        await runner.RunAsync(hostConfig.Id, MatrixType.RectMatrix, 2000,
            RectMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_2000x2000_A.bin")),
            RectMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_2000x2000_B.bin"))).ConfigureAwait(false);
        await runner.RunAsync(hostConfig.Id, MatrixType.JaggedMatrix, 2000,
            JaggedMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_2000x2000_A.bin")),
            JaggedMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_2000x2000_B.bin"))).ConfigureAwait(false);
        await runner.RunAsync(hostConfig.Id, MatrixType.FlatMatrix, 2000,
            FlatMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_2000x2000_A.bin")),
            FlatMatrix<double>.LoadFromBinaresFile(Path.Combine(matrixDir, "matrix_2000x2000_B.bin"))).ConfigureAwait(false);

        BreakEvenAnalyzer.Analyze();
    }
}