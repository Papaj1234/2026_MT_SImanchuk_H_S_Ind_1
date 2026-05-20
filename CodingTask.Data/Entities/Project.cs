using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("Projects")]
public class Project : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string FolderPath { get; set; } = string.Empty;

    public ICollection<BuildStage> BuildStages { get; } = new List<BuildStage>();
}