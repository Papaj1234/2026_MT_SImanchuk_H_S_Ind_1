using CodingTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class StageTypeRepository : Repository<StageType>
{
    public StageTypeRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<StageType?> GetByNameAsync(string name) => await this.DbSet.FirstOrDefaultAsync(s => s.Name == name).ConfigureAwait(false);
}