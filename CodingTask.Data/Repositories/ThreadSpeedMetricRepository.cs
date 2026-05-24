using CodingTask.Data.Entities;
using CodingTask.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class ThreadSpeedMetricRepository : Repository<ThreadSpeedMetric>, IThreadSpeedMetricRepository
{
    public ThreadSpeedMetricRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<ThreadSpeedMetric>> GetByBuildStageIdAsync(int buildStageId) =>
        await this.DbSet
            .Include(t => t.HostConfiguration)
            .Where(t => t.BuildStageId == buildStageId)
            .ToListAsync().ConfigureAwait(false);
}