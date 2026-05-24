using CodingTask.Data.Entities;

namespace CodingTask.Data.Seeding;

public class DefaultDataFactory : IDataFactory
{
    private readonly string projectPath;

    public DefaultDataFactory(string projectPath)
    {
        this.projectPath = projectPath;
    }

    public Project CreateProject() => new ()
    {
        Name = "CodingTask",
        FolderPath = this.projectPath,
    };

    public BuildStatus CreateBuildStatus(string name) => new ()
    {
        Name = name,
    };

    public IssueSeverity CreateIssueSeverity(string name) => new ()
    {
        Name = name,
    };

    public StageType CreateStageType(string name) => new ()
    {
        Name = name,
    };

    public BuildStage CreateBuildStage(int projectId, int stageTypeId, int statusId) => new ()
    {
        ProjectId = projectId,
        StageTypeId = stageTypeId,
        StatusId = statusId,
        StartedAt = DateTime.UtcNow,
        DurationMs = 1500,
        ErrorCount = 0,
        WarningCount = 2,
    };

    public IssueLog CreateIssueLog(int buildStageId, int severityId) => new ()
    {
        BuildStageId = buildStageId,
        SeverityId = severityId,
        LoggedAt = DateTime.UtcNow,
        Code = "CS0168",
        Message = "The variable 'x' is declared but never used",
    };

    public CpuModel CreateCpuModel() => new ()
    {
        Manufacturer = "Intel",
        Model = "Core i7-12700K",
        PhysicalCoreCount = 12,
        LogicalThreadCount = 20,
    };

    public Entities.OperatingSystem CreateOperatingSystem() => new ()
    {
        Name = "Windows",
        Version = "11 Pro",
    };

    public HostConfiguration CreateHostConfiguration(int cpuModelId, int operatingSystemId) => new ()
    {
        CpuModelId = cpuModelId,
        RamGb = 32.0m,
        OperatingSystemId = operatingSystemId,
    };

    public ThreadSpeedMetric CreateThreadSpeedMetric(int buildStageId, int hostConfigId) => new ()
    {
        BuildStageId = buildStageId,
        HostConfigurationId = hostConfigId,
        Description = "Matrix Multiplication 2000x2000",
        MeasuredAt = DateTime.UtcNow,
        ParallelThreadCount = 8,
        SequentialTimeMs = 4200,
        ParallelTimeMs = 620,
    };
}