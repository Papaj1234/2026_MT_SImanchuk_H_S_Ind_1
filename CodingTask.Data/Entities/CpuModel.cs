using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("CpuModels")]
public class CpuModel : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Manufacturer { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Model { get; set; } = string.Empty;

    public int PhysicalCoreCount { get; set; }

    public int LogicalThreadCount { get; set; }

    public ICollection<HostConfiguration> HostConfigurations { get; } = new List<HostConfiguration>();
}