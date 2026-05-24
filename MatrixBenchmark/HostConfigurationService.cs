using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CodingTask.Data.Entities;
using CodingTask.Data.Infrastructure;

namespace MatrixBenchmark
{
    internal sealed class HostConfigurationService
    {
        private readonly IUnitOfWork uow;

        internal HostConfigurationService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        internal async Task<HostConfiguration> GetOrCreateAsync()
        {
            var cpuModel = new CpuModel
            {
                Manufacturer = "Intel",
                Model = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") ?? "Unknown",
                PhysicalCoreCount = Environment.ProcessorCount / 2,
                LogicalThreadCount = Environment.ProcessorCount,
            };
            await this.uow.CpuModels.AddAsync(cpuModel).ConfigureAwait(false);
            await this.uow.SaveChangesAsync().ConfigureAwait(false);

            var os = new CodingTask.Data.Entities.OperatingSystem
            {
                Name = RuntimeInformation.OSDescription,
                Version = Environment.OSVersion.Version.ToString(),
            };
            await this.uow.OperatingSystems.AddAsync(os).ConfigureAwait(false);
            await this.uow.SaveChangesAsync().ConfigureAwait(false);

            var hostConfig = new HostConfiguration
            {
                CpuModelId = cpuModel.Id,
                RamGb = 16,
                OperatingSystemId = os.Id,
            };
            await this.uow.HostConfigurations.AddAsync(hostConfig).ConfigureAwait(false);
            await this.uow.SaveChangesAsync().ConfigureAwait(false);

            return hostConfig;
        }
    }
}