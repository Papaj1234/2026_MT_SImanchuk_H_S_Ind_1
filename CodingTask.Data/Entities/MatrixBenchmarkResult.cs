using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("MatrixBenchmarkResults")]
public class MatrixBenchmarkResult : BaseEntity
{
    public int HostConfigurationId { get; set; }

    [ForeignKey(nameof(HostConfigurationId))]
    public HostConfiguration HostConfiguration { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string MatrixType { get; set; } = string.Empty;

    public int MatrixSize { get; set; }

    [Required]
    [MaxLength(100)]
    public string MethodName { get; set; } = string.Empty;

    public long ElapsedMs { get; set; }

    public DateTime MeasuredAt { get; set; }
}