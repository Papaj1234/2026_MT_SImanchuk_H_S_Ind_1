using CodingTask.Data.Entities;

namespace CodingTask.Data.Seeding;

public interface IDataFactory
{
    Project CreateProject();

    BuildStatus CreateBuildStatus(string name);

    IssueSeverity CreateIssueSeverity(string name);

    StageType CreateStageType(string name);

    BuildStage CreateBuildStage(int projectId, int stageTypeId, int statusId);

    IssueLog CreateIssueLog(int buildStageId, int severityId);

    HostConfiguration CreateHostConfiguration();

    ThreadSpeedMetric CreateThreadSpeedMetric(int buildStageId, int hostConfigId);
}