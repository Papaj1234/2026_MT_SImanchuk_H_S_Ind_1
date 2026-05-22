using CodingTask.Data.Entities;
using CodingTask.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class StageTypeRepository : Repository<StageType>, IStageTypeRepository
{
    public StageTypeRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<StageType?> GetByNameAsync(string name) => await this.DbSet.FirstOrDefaultAsync(s => s.Name == name).ConfigureAwait(false);
}