using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTask.CICDSync.Models;

public class PipelineStep
{
    public string Name { get; set; } = string.Empty;

    public string Command { get; set; } = string.Empty;

    public string Args { get; set; } = string.Empty;

    public bool StopOnFailure { get; set; }
}