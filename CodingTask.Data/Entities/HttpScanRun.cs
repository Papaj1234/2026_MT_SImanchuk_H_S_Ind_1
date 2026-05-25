using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("HttpScanRuns")]
public class HttpScanRun : BaseEntity
{
    public OperationType OperationType { get; set; }

    public int MaxParallelism { get; set; }

    public long ElapsedMs { get; set; }

    public int SuccessCount { get; set; }

    public int ErrorCount { get; set; }

    public DateTime MeasuredAt { get; set; }

    public int HostConfigurationId { get; set; }

    [ForeignKey(nameof(HostConfigurationId))]
    public HostConfiguration HostConfiguration { get; set; } = null!;
}