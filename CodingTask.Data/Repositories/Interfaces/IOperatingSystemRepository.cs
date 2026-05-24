using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IOperatingSystemRepository : IRepository<Entities.OperatingSystem>
{
    Task<CodingTask.Data.Entities.OperatingSystem?> GetExistingAsync(string name, string version);
}