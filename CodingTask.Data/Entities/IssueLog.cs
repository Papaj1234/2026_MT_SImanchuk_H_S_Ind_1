using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("IssueLogs")]
public class IssueLog : BaseEntity
{
    public int BuildStageId { get; set; }

    [ForeignKey(nameof(BuildStageId))]
    public BuildStage BuildStage { get; set; } = null!;

    public int SeverityId { get; set; }

    [ForeignKey(nameof(SeverityId))]
    public IssueSeverity Severity { get; set; } = null!;

    public DateTime LoggedAt { get; set; }

    [MaxLength(50)]
    public string? Code { get; set; }

    [Required]
    public string Message { get; set; } = string.Empty;
}