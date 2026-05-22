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

    public async Task<HostConfiguration?> GetExistingAsync(string cpuModel, int physicalCores, int logicalThreads, decimal ramGb, string os) =>
        await this.DbSet.FirstOrDefaultAsync(h =>
            h.CpuModel == cpuModel &&
            h.PhysicalCoreCount == physicalCores &&
            h.LogicalThreadCount == logicalThreads &&
            h.RamGb == ramGb &&
            h.OperatingSystem == os).ConfigureAwait(false);
}