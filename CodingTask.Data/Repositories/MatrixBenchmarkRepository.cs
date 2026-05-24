using CodingTask.Data.Entities;
using CodingTask.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class MatrixBenchmarkRepository : Repository<MatrixBenchmarkResult>, IMatrixBenchmarkRepository
{
    public MatrixBenchmarkRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<MatrixBenchmarkResult>> GetByMatrixTypeAsync(string matrixType) =>
        await this.DbSet
            .Include(m => m.HostConfiguration)
            .Where(m => m.MatrixType == matrixType)
            .ToListAsync().ConfigureAwait(false);
}