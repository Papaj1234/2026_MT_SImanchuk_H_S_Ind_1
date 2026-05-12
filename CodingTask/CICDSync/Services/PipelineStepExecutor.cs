using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodingTask.CICDSync.Interfaces;
using CodingTask.CICDSync.Models;

namespace CodingTask.CICDSync.Services;

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
        this.logger.Info($"Starting stage: {step?.Name}");

        int exitCode = this.executor.Execute(step.Command, step.Args, targetDir);

        if (exitCode == 0)
        {
            this.logger.Success($"Stage '{step.Name}' finished with ExitCode {exitCode}");
            return true;
        }

        this.logger.LogError($"Stage '{step.Name}' failed with ExitCode {exitCode}");
        return false;
    }
}