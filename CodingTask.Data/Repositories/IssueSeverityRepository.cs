using CodingTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class IssueSeverityRepository : Repository<IssueSeverity>
{
    public IssueSeverityRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IssueSeverity?> GetByNameAsync(string name) =>
        await this.DbSet.FirstOrDefaultAsync(s => s.Name == name).ConfigureAwait(false);
}