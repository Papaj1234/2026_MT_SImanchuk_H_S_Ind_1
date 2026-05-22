using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IThreadSpeedMetricRepository : IRepository<ThreadSpeedMetric>
{
    Task<IEnumerable<ThreadSpeedMetric>> GetByBuildStageIdAsync(int buildStageId);
}