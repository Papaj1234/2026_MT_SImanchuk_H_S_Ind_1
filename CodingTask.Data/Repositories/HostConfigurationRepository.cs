using CodingTask.Data.Entities;
using CodingTask.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class HostConfigurationRepository : Repository<HostConfiguration>, IHostConfigurationRepository
{
    public HostConfigurationRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<HostConfiguration?> GetExistingAsync(int cpuModelId, decimal ramGb, int operatingSystemId) =>
        await this.DbSet.FirstOrDefaultAsync(h =>
            h.CpuModelId == cpuModelId &&
            h.RamGb == ramGb &&
            h.OperatingSystemId == operatingSystemId).ConfigureAwait(false);
}