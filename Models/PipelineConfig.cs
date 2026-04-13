using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDSync.Models;

public class PipelineConfig
{
    public List<PipelineStep> Pipeline { get; set; } = new();
}
