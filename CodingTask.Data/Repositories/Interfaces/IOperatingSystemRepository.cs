using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IOperatingSystemRepository : IRepository<Entities.OperatingSystem>
{
    Task<OperatingSystem?> GetExistingAsync(string name, string version);
}