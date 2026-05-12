using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodingTask.CICDSync.Interfaces;
using CodingTask.CICDSync.Models;

namespace CodingTask.CICDSync.Services;

public class PipelineRunner
{
    private readonly PipelineStepExecutor stepExecutor;
    private readonly PipelineConfigValidator validator;
    private readonly ILogger logger;

    public PipelineRunner(PipelineStepExecutor stepExecutor, PipelineConfigValidator validator, ILogger logger)
    {
        this.stepExecutor = stepExecutor;
        this.validator = validator;
        this.logger = logger;
    }

    public void Run(PipelineConfig config, string targetDir)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(targetDir);

        if (!this.validator.Validate(config))
        {
            return;
        }

        bool success = this.RunPipeline(config!, targetDir);
        this.LogFinalResult(success);
    }

    private bool RunPipeline(PipelineConfig config, string targetDir)
    {
        foreach (var step in config.Pipeline)
        {
            bool success = this.stepExecutor.Execute(step, targetDir);
            if (!success && step.StopOnFailure)
            {
                this.logger.LogError("Stopping pipeline.");
                return false;
            }
        }

        return true;
    }

    private void LogFinalResult(bool success)
    {
        if (success)
        {
            this.logger.Success("Pipeline completed successfully.");
        }
        else
        {
            this.logger.LogError("Pipeline finished with errors.");
        }
    }
}