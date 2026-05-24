using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IIssueLogRepository : IRepository<IssueLog>
{
    Task<IEnumerable<IssueLog>> GetByBuildStageIdAsync(int buildStageId);
}