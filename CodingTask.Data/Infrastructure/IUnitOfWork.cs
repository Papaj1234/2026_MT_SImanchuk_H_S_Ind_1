using CodingTask.Data.Repositories;

namespace CodingTask.Data.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    ProjectRepository Projects { get; }

    StageTypeRepository StageTypes { get; }

    BuildStatusRepository BuildStatuses { get; }

    IssueSeverityRepository IssueSeverities { get; }

    BuildStageRepository BuildStages { get; }

    IssueLogRepository IssueLogs { get; }

    HostConfigurationRepository HostConfigurations { get; }

    ThreadSpeedMetricRepository ThreadSpeedMetrics { get; }

    Task<int> SaveChangesAsync();
}