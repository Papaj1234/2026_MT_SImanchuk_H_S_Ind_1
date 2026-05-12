using System;
using System.Globalization;
using System.IO;
using System.Text;
using CodingTask.CICDSync.Interfaces;

namespace CodingTask.CICDSync.Services;

public class Logger : ILogger
{
    private static readonly CompositeFormat FileNameFormat = CompositeFormat.Parse("CICD_{0}_{1:yyyy_MM_dd_HH_mm_ss}.log");
    private static readonly CompositeFormat LogFormat = CompositeFormat.Parse("[{0:yyyy-MM-dd HH:mm:ss}] [{1}] {2}");

    private readonly string logFile;

    public Logger(string targetDir)
    {
        ArgumentNullException.ThrowIfNull(targetDir);

        string dirName = new DirectoryInfo(targetDir).Name;

        string fileName = string.Format(CultureInfo.InvariantCulture, FileNameFormat, dirName, DateTime.Now);

        this.logFile = Path.Combine(targetDir, fileName);
    }

    public void Info(string message)
    {
        this.Write("INFO", message);
    }

    public void Success(string message)
    {
        this.Write("SUCCESS", message);
    }

    public void LogError(string message)
    {
        this.Write("ERROR", message);
    }

    private void Write(string level, string message)
    {
        string line = string.Format(CultureInfo.InvariantCulture, LogFormat, DateTime.Now, level, message);
        Console.WriteLine(line);

        using (var writer = new StreamWriter(this.logFile, append: true))
        {
            writer.WriteLine(line);
        }
    }
}