using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IHostConfigurationRepository : IRepository<HostConfiguration>
{
    Task<HostConfiguration?> GetExistingAsync(string cpuModel, int physicalCores, int logicalThreads, decimal ramGb, string os);
}