using CICDSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDSync.Services;

public class PipelineConfigValidator
{
    private readonly PipelineStepExecutor stepExecutor;
    private readonly ILogger logger;

    public PipelineConfigValidator(PipelineStepExecutor stepExecutor, ILogger logger)
    {
        this.stepExecutor = stepExecutor;
        this.logger = logger;
    }

    public void Run(PipelineConfig? config, string targetDir)
    {
        if (!IsValidConfig(config))
        {
            return;
        }
            

        bool result = RunPipeline(config!, targetDir);

        LogFinalResult(result);
    }

    private bool IsValidConfig(PipelineConfig? config)
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

    private bool RunPipeline(PipelineConfig config, string targetDir)
    {
        foreach (var step in config.Pipeline)
        {
            bool success = stepExecutor.Execute(step, targetDir);

            if (!success && step.StopOnFailure)
            {
                logger.Error("Stopping pipeline.");
                return false;
            }
        }

        return true;
    }

    private void LogFinalResult(bool success)
    {
        if (success)
        {
            logger.Success("Pipeline completed successfully.");
        }

        else
        {
            logger.Error("Pipeline finished with errors.");
        }    
    }
}