namespace CICDSync.Services;

public interface ILogger
{
    void Info(string message);
    void Success(string message);
    void Error(string message);
}