using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IBuildStatusRepository : IRepository<BuildStatus>
{
    Task<BuildStatus?> GetByNameAsync(string name);
}