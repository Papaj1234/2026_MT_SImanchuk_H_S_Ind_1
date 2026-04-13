using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDSync.Services;

public class Logger : ILogger
{
    private const string FileNameFormat = "CICD_{0}_{1:yyyy_MM_dd_HH_mm_ss}.log";
    private const string LogFormat = "[{0:yyyy-MM-dd HH:mm:ss}] [{1}] {2}";
    private readonly string logFile;

    public Logger(string targetDir)
    {
        string dirName = new DirectoryInfo(targetDir).Name;
        string fileName = string.Format(FileNameFormat, dirName, DateTime.Now);
        string logDir = targetDir;
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
        string line = string.Format(LogFormat, DateTime.Now, level, message);
        Console.WriteLine(line);

        using var writer = new StreamWriter(logFile, append: true);
        writer.WriteLine(line);
    }
}