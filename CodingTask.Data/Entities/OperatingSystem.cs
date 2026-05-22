using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("OperatingSystems")]
public class OperatingSystem : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Version { get; set; } = string.Empty;

    public ICollection<HostConfiguration> HostConfigurations { get; } = new List<HostConfiguration>();
}