using CodingTask.Data.Infrastructure;

namespace CodingTask.Data.Seeding;

public class DatabaseSeeder
{
    private readonly IUnitOfWork uow;
    private readonly IDataFactory factory;

    public DatabaseSeeder(IUnitOfWork uow, IDataFactory factory)
    {
        this.uow = uow;
        this.factory = factory;
    }

    public async Task SeedAsync()
    {
        var statuses = new[] { "success", "failed" };
        foreach (var s in statuses)
        {
            await this.uow.BuildStatuses.AddAsync(this.factory.CreateBuildStatus(s)).ConfigureAwait(false);
        }

        var severities = new[] { "Error", "Warning" };
        foreach (var s in severities)
        {
            await this.uow.IssueSeverities.AddAsync(this.factory.CreateIssueSeverity(s)).ConfigureAwait(false);
        }

        var stageNames = new[] { "Build", "Test", "Clean", "Run" };
        foreach (var s in stageNames)
        {
            await this.uow.StageTypes.AddAsync(this.factory.CreateStageType(s)).ConfigureAwait(false);
        }

        await this.uow.SaveChangesAsync().ConfigureAwait(false);

        var project = this.factory.CreateProject();
        await this.uow.Projects.AddAsync(project).ConfigureAwait(false);
        await this.uow.SaveChangesAsync().ConfigureAwait(false);

        var stageTypes = (await this.uow.StageTypes.GetAllAsync().ConfigureAwait(false)).ToList();
        var successStatus = (await this.uow.BuildStatuses.GetAllAsync().ConfigureAwait(false)).First(s => s.Name == "success");

        var buildStage = this.factory.CreateBuildStage(project.Id, stageTypes[0].Id, successStatus.Id);
        await this.uow.BuildStages.AddAsync(buildStage).ConfigureAwait(false);
        await this.uow.SaveChangesAsync().ConfigureAwait(false);

        var warningSeverity = (await this.uow.IssueSeverities.GetAllAsync().ConfigureAwait(false)).First(s => s.Name == "Warning");
        var log = this.factory.CreateIssueLog(buildStage.Id, warningSeverity.Id);
        await this.uow.IssueLogs.AddAsync(log).ConfigureAwait(false);

        var cpuModel = this.factory.CreateCpuModel();
        await this.uow.CpuModels.AddAsync(cpuModel).ConfigureAwait(false);
        await this.uow.SaveChangesAsync().ConfigureAwait(false);

        var operatingSystem = this.factory.CreateOperatingSystem();
        await this.uow.OperatingSystems.AddAsync(operatingSystem).ConfigureAwait(false);
        await this.uow.SaveChangesAsync().ConfigureAwait(false);

        var host = this.factory.CreateHostConfiguration(cpuModel.Id, operatingSystem.Id);
        await this.uow.HostConfigurations.AddAsync(host).ConfigureAwait(false);
        await this.uow.SaveChangesAsync().ConfigureAwait(false);

        var metric = this.factory.CreateThreadSpeedMetric(buildStage.Id, host.Id);
        await this.uow.ThreadSpeedMetrics.AddAsync(metric).ConfigureAwait(false);
        await this.uow.SaveChangesAsync().ConfigureAwait(false);
    }
}