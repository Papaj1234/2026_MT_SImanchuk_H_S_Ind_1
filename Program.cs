using CICDSync.Models;
using CICDSync.Services;
using System.Text.Json;

namespace CICD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: CICDSync <config.json> <target_dir>");
                return;
            }

            string configPath = args[0];
            string targetDir = args[1];

            if (!File.Exists(configPath))
            {
                Console.WriteLine("Config file not found.");
                return;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var config = JsonSerializer.Deserialize<PipelineConfig>(
                File.ReadAllText(configPath),
                options
            );

            var workDir = Path.GetDirectoryName(Path.GetFullPath(configPath))!;

            var logger = new Logger(workDir);
            var executor = new CommandExecutor(logger);
            var stepExecutor = new PipelineStepExecutor(executor, logger);
            var runner = new PipelineConfigValidator(stepExecutor, logger);

            runner.Run(config, targetDir);
        }
    }
}