using CodingTask.Data.Repositories;

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
    }

    public ProjectRepository Projects { get; }

    public StageTypeRepository StageTypes { get; }

    public BuildStatusRepository BuildStatuses { get; }

    public IssueSeverityRepository IssueSeverities { get; }

    public BuildStageRepository BuildStages { get; }

    public IssueLogRepository IssueLogs { get; }

    public HostConfigurationRepository HostConfigurations { get; }

    public ThreadSpeedMetricRepository ThreadSpeedMetrics { get; }

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