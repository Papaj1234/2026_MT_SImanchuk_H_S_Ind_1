using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("BuildStages")]
public class BuildStage : BaseEntity
{
    public int ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))]
    public Project Project { get; set; } = null!;

    public int StageTypeId { get; set; }

    [ForeignKey(nameof(StageTypeId))]
    public StageType StageType { get; set; } = null!;

    public int StatusId { get; set; }

    [ForeignKey(nameof(StatusId))]
    public BuildStatus Status { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public long DurationMs { get; set; }

    public int ErrorCount { get; set; }

    public int WarningCount { get; set; }

    public ICollection<IssueLog> IssueLogs { get; } = new List<IssueLog>();

    public ICollection<ThreadSpeedMetric> ThreadSpeedMetrics { get; } = new List<ThreadSpeedMetric>();
}