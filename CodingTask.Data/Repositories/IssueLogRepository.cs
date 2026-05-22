using CodingTask.Data.Entities;
using CodingTask.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class IssueLogRepository : Repository<IssueLog>, IIssueLogRepository
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