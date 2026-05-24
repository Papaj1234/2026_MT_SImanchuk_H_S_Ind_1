using CodingTask.Data.Entities;
using CodingTask.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class CpuModelRepository : Repository<CpuModel>, ICpuModelRepository
{
    public CpuModelRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<CpuModel?> GetExistingAsync(string manufacturer, string model) =>
        await this.DbSet.FirstOrDefaultAsync(c =>
            c.Manufacturer == manufacturer &&
            c.Model == model).ConfigureAwait(false);
}