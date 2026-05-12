using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodingTask.CICDSync.Interfaces;

namespace CodingTask.CICDSync.Services;

public class CommandExecutor : ICommandExecutor
{
    private readonly ILogger logger;

    public CommandExecutor(ILogger logger)
    {
        this.logger = logger;
    }

    public int Execute(string command, string args, string workingDir)
    {
        try
        {
            using var process = new Process();

            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = args;
            process.StartInfo.WorkingDirectory = workingDir;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.OutputDataReceived += (_, e) =>
            {
                if (e.Data != null)
                {
                    this.logger.Info(e.Data);
                }
            };
            process.ErrorDataReceived += (_, e) =>
            {
                if (e.Data != null)
                {
                    this.logger.LogError(e.Data);
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            return process.ExitCode;
        }
        catch (System.ComponentModel.Win32Exception ex)
        {
            this.logger.LogError($"Failed to execute '{command}': {ex.Message}");
            return -1;
        }
        catch (InvalidOperationException ex)
        {
            this.logger.LogError($"Failed to execute '{command}': {ex.Message}");
            return -1;
        }
    }
}