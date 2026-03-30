using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDSync.Services;

public class Logger : ILogger
{
    private const string TimestampFormat = "yyyy-MM-dd HH:mm:ss";

    private const string FileTimestampFormat = "yyyy_MM_dd_HH_mm_ss";

    private readonly string logFile;

    public Logger(string targetDir)
    {
        string dirName = new DirectoryInfo(targetDir).Name;

        string fileName = $"CICD_{dirName}_{DateTime.Now.ToString(FileTimestampFormat)}.log";

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
        string line = $"[{DateTime.Now.ToString(TimestampFormat)}] [{level}] {message}";

        Console.WriteLine(line);

        File.AppendAllText(logFile, line + Environment.NewLine);

    }
}