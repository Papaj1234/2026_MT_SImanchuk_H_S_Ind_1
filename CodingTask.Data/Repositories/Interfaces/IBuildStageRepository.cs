using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IBuildStageRepository : IRepository<BuildStage>
{
    Task<IEnumerable<BuildStage>> GetByProjectIdAsync(int projectId);
}