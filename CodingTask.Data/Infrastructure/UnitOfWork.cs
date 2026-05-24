using CodingTask.Data.Repositories;
using CodingTask.Data.Repositories.Interfaces;

namespace CodingTask.Data.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    private bool disposed;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        this.Projects = new ProjectRepository(context);
        this.StageTypes = new StageTypeRepository(context);
        this.BuildStatuses = new BuildStatusRepository(context);
        this.IssueSeverities = new IssueSeverityRepository(context);
        this.BuildStages = new BuildStageRepository(context);
        this.IssueLogs = new IssueLogRepository(context);
        this.HostConfigurations = new HostConfigurationRepository(context);
        this.ThreadSpeedMetrics = new ThreadSpeedMetricRepository(context);
        this.CpuModels = new CpuModelRepository(context);
        this.OperatingSystems = new OperatingSystemRepository(context);
    }

    public IProjectRepository Projects { get; }

    public IStageTypeRepository StageTypes { get; }

    public IBuildStatusRepository BuildStatuses { get; }

    public IIssueSeverityRepository IssueSeverities { get; }

    public IBuildStageRepository BuildStages { get; }

    public IIssueLogRepository IssueLogs { get; }

    public IHostConfigurationRepository HostConfigurations { get; }

    public IThreadSpeedMetricRepository ThreadSpeedMetrics { get; }

    public ICpuModelRepository CpuModels { get; }

    public IOperatingSystemRepository OperatingSystems { get; }

    public async Task<int> SaveChangesAsync() =>
        await this.context.SaveChangesAsync().ConfigureAwait(false);

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.context.Dispose();
            }

            this.disposed = true;
        }
    }
}