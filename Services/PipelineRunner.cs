using CICDSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDSync.Services;

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

	public void Run(PipelineConfig? config, string targetDir)
	{
		if (!validator.Validate(config))
		{
			return;
		}

		bool success = RunPipeline(config!, targetDir);
		LogFinalResult(success);
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