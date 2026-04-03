using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CICDSync.Models;

namespace CICDSync.Services;

public class PipelineStepExecutor
{
    private readonly ICommandExecutor executor;
    private readonly ILogger logger;

    public PipelineStepExecutor(ICommandExecutor executor, ILogger logger)
    {
        this.executor = executor;
        this.logger = logger;
    }

    public bool Execute(PipelineStep step, string targetDir)
    {
        logger.Info($"Starting stage: {step.Name}");

        int exitCode = executor.Execute(step.Command, step.Args, targetDir);

        if (exitCode == 0)
        {
            logger.Success($"Stage '{step.Name}' finished with ExitCode {exitCode}");
            return true;
        }

        logger.Error($"Stage '{step.Name}' failed with ExitCode {exitCode}");
        return false;
    }
}