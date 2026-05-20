using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("BuildStatuses")]
public class BuildStatus : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    public ICollection<BuildStage> BuildStages { get; } = new List<BuildStage>();
}