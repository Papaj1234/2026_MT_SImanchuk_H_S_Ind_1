using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IStageTypeRepository : IRepository<StageType>
{
    Task<StageType?> GetByNameAsync(string name);
}