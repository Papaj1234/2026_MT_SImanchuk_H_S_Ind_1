using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDSync.Models;

public class PipelineStep
{
    public string Name { get; set; } = String.Empty;
    public string Command { get; set; } = String.Empty;
    public string Args { get; set; } = String.Empty;
    public bool StopOnFailure { get; set; }
}
