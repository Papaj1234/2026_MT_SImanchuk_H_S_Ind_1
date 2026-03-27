using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDSync.Services;

public class Logger
{
    private readonly string logFile;

    public Logger(string targetDir)
    {
        string dirName = new DirectoryInfo(targetDir).Name;

        string fileName = $"CICD_{dirName}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.log";

        string logDir = Directory.GetParent(targetDir)!.FullName;

        logFile = Path.Combine(logDir, fileName);
    }

    public void Info(string message)
    {
        Write("INFO", message);
    }

    public void Success(string message)
    {
        Write("SUCCESS", message);
    }

    public void Error(string message)
    {
        Write("ERROR", message);
    }

    private void Write(string level, string message)
    {
        string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

        Console.WriteLine(line);

        File.AppendAllText(logFile, line + Environment.NewLine);
    }
}
