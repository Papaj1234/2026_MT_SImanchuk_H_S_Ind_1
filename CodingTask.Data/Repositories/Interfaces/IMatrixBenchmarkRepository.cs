using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IMatrixBenchmarkRepository : IRepository<MatrixBenchmarkResult>
{
    Task<IEnumerable<MatrixBenchmarkResult>> GetByMatrixTypeAsync(MatrixType matrixType);
}