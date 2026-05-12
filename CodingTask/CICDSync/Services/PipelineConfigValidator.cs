using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodingTask.CICDSync.Interfaces;
using CodingTask.CICDSync.Models;

namespace CodingTask.CICDSync.Services;

public class PipelineConfigValidator
{
    private readonly ILogger logger;

    public PipelineConfigValidator(ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        this.logger = logger;
    }

    public bool Validate(PipelineConfig? config)
    {
        if (config is null)
        {
            this.logger.LogError("Config is null.");
            return false;
        }

        if (config.Pipeline.Count == 0)
        {
            this.logger.Info("Pipeline is empty, nothing to run.");
            return false;
        }

        return true;
    }
}