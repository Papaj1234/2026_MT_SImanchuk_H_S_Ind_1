using CodingTask.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class OperatingSystemRepository : Repository<Entities.OperatingSystem>, IOperatingSystemRepository
{
    public OperatingSystemRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<Entities.OperatingSystem?> GetExistingAsync(string name, string version) =>
        await this.DbSet.FirstOrDefaultAsync(o =>
            o.Name == name &&
            o.Version == version).ConfigureAwait(false);
}