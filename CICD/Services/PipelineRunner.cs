using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CICDSync.Models;

namespace CICDSync.Services;

public class PipelineRunner
{
    private readonly CommandExecutor executor;
    private readonly Logger logger;

    public PipelineRunner(CommandExecutor executor, Logger logger)
    {
        this.executor = executor;
        this.logger = logger;
    }

    public void Run(PipelineConfig? config, string targetDir)
    {
        if (config is null)
        {
            logger.Error("Config is null.");
            return;
        }

        foreach (var step in config.Pipeline)
        {
            logger.Info($"Starting stage: {step.Name}");

            int exitCode = executor.Execute(step.Command, step.Args, targetDir);

            if (exitCode == 0)
            {
                logger.Success($"Stage '{step.Name}' finished with ExitCode {exitCode}");
            }
            else
            {
                logger.Error($"Stage '{step.Name}' failed with ExitCode {exitCode}");

                if (step.StopOnFailure)
                {
                    logger.Error("Stopping pipeline.");
                    break;
                }
            }
        }
    }
}