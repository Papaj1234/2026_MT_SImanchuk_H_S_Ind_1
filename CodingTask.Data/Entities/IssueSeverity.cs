using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("IssueSeverities")]
public class IssueSeverity : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    public ICollection<IssueLog> IssueLogs { get; } = new List<IssueLog>();
}