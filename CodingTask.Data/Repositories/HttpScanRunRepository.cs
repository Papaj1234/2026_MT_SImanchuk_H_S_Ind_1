using CodingTask.Data.Entities;
using CodingTask.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class HttpScanRunRepository : Repository<HttpScanRun>, IHttpScanRunRepository
{
    public HttpScanRunRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<HttpScanRun>> GetByOperationTypeAsync(OperationType operationType)
    {
        return await this.DbSet
            .Where(x => x.OperationType == operationType)
            .ToListAsync()
            .ConfigureAwait(false);
    }
}