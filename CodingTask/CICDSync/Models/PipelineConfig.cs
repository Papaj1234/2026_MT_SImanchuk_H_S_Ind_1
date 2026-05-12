using System.Collections.ObjectModel;

namespace CodingTask.CICDSync.Models;

public class PipelineConfig
{
    public Collection<PipelineStep> Pipeline { get; } = new ();
}