namespace CICDSync.Services;

public interface ICommandExecutor
{
    int Execute(string command, string args, string workingDir);
}