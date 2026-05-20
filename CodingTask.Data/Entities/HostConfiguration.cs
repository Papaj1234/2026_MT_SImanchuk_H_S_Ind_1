using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("HostConfigurations")]
public class HostConfiguration : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string CpuModel { get; set; } = string.Empty;

    public int PhysicalCoreCount { get; set; }

    public int LogicalThreadCount { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal RamGb { get; set; }

    [Required]
    [MaxLength(200)]
    public string OperatingSystem { get; set; } = string.Empty;

    public ICollection<ThreadSpeedMetric> ThreadSpeedMetrics { get; } = new List<ThreadSpeedMetric>();
}