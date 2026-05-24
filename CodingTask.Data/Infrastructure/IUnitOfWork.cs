using CodingTask.Data.Repositories;
using CodingTask.Data.Repositories.Interfaces;

namespace CodingTask.Data.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    IProjectRepository Projects { get; }

    IStageTypeRepository StageTypes { get; }

    IBuildStatusRepository BuildStatuses { get; }

    IIssueSeverityRepository IssueSeverities { get; }

    IBuildStageRepository BuildStages { get; }

    IIssueLogRepository IssueLogs { get; }

    IHostConfigurationRepository HostConfigurations { get; }

    IThreadSpeedMetricRepository ThreadSpeedMetrics { get; }

    ICpuModelRepository CpuModels { get; }

    IOperatingSystemRepository OperatingSystems { get; }

    Task<int> SaveChangesAsync();
}