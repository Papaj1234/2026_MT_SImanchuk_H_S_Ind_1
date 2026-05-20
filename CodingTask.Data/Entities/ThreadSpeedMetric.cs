using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("ThreadSpeedMetrics")]
public class ThreadSpeedMetric : BaseEntity
{
    public int BuildStageId { get; set; }

    [ForeignKey(nameof(BuildStageId))]
    public BuildStage BuildStage { get; set; } = null!;

    public int HostConfigurationId { get; set; }

    [ForeignKey(nameof(HostConfigurationId))]
    public HostConfiguration HostConfiguration { get; set; } = null!;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime MeasuredAt { get; set; }

    public int ParallelThreadCount { get; set; }

    public long SequentialTimeMs { get; set; }

    public long ParallelTimeMs { get; set; }
}