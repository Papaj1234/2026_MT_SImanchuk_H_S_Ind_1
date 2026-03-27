using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDSync.Services;

public class CommandExecutor
{
    private readonly Logger logger;

    public CommandExecutor(Logger logger)
    {
        this.logger = logger;
    }

    public int Execute(string command, string args, string workingDir)
    {
        var process = new Process();
        process.StartInfo.FileName = command;
        process.StartInfo.Arguments = args;
        process.StartInfo.WorkingDirectory = workingDir;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;

        process.OutputDataReceived += (_, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
        process.ErrorDataReceived += (_, e) => { if (e.Data != null) Console.Error.WriteLine(e.Data); };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        return process.ExitCode;
    }
}
