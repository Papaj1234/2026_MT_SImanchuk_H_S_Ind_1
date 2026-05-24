using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("HostConfigurations")]
public class HostConfiguration : BaseEntity
{
    public int CpuModelId { get; set; }

    [ForeignKey(nameof(CpuModelId))]
    public CpuModel CpuModel { get; set; } = null!;

    [Column(TypeName = "decimal(5,2)")]
    public decimal RamGb { get; set; }

    public int OperatingSystemId { get; set; }

    [ForeignKey(nameof(OperatingSystemId))]
    public OperatingSystem OperatingSystem { get; set; } = null!;

    public ICollection<ThreadSpeedMetric> ThreadSpeedMetrics { get; } = new List<ThreadSpeedMetric>();
}