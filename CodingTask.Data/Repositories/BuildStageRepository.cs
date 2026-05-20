using CodingTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class BuildStageRepository : Repository<BuildStage>
{
    public BuildStageRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<BuildStage>> GetByProjectIdAsync(int projectId) => await this.DbSet
            .Include(b => b.StageType)
            .Include(b => b.Status)
            .Include(b => b.IssueLogs)
            .Where(b => b.ProjectId == projectId)
            .ToListAsync().ConfigureAwait(false);
}