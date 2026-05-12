namespace CodingTask.CICDSync.Interfaces;

public interface ILogger
{
    void Info(string message);

    void Success(string message);

    void LogError(string message);
}