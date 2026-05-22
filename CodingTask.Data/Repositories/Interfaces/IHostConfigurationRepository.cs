using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IHostConfigurationRepository : IRepository<HostConfiguration>
{
    Task<HostConfiguration?> GetExistingAsync(int cpuModelId, decimal ramGb, int operatingSystemId);
}