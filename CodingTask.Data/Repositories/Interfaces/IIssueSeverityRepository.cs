using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IIssueSeverityRepository : IRepository<IssueSeverity>
{
    Task<IssueSeverity?> GetByNameAsync(string name);
}