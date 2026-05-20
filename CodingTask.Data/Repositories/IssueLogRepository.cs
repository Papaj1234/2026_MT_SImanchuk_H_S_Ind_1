using CodingTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class IssueLogRepository : Repository<IssueLog>
{
    public IssueLogRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<IssueLog>> GetByBuildStageIdAsync(int buildStageId) =>
        await this.DbSet
            .Include(l => l.Severity)
            .Where(l => l.BuildStageId == buildStageId)
            .ToListAsync().ConfigureAwait(false);
}