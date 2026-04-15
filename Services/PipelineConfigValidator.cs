using CICDSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDSync.Services;

public class PipelineConfigValidator
{
    private readonly ILogger logger;

    public PipelineConfigValidator(ILogger logger)
    {
        this.logger = logger;
    }

    public bool Validate(PipelineConfig? config)
    {
        if (config is null)
        {
            logger.Error("Config is null.");
            return false;
        }

        if (!config.Pipeline.Any())
        {
            logger.Info("Pipeline is empty, nothing to run.");
            return false;
        }

        return true;
    }
}