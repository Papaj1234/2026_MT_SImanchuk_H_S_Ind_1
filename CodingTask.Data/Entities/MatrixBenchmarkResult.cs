using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTask.Data.Entities;

[Table("MatrixBenchmarkResults")]
public class MatrixBenchmarkResult : BaseEntity
{
    public int HostConfigurationId { get; set; }

    [ForeignKey(nameof(HostConfigurationId))]
    public HostConfiguration HostConfiguration { get; set; } = null!;

    public MatrixType MatrixType { get; set; }

    public int MatrixSize { get; set; }

    public MatrixMethod MethodName { get; set; }

    public long ElapsedMs { get; set; }

    public DateTime MeasuredAt { get; set; }
}