using CodingTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class BuildStatusRepository : Repository<BuildStatus>
{
    public BuildStatusRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<BuildStatus?> GetByNameAsync(string name) =>
        await this.DbSet.FirstOrDefaultAsync(s => s.Name == name).ConfigureAwait(false);
}