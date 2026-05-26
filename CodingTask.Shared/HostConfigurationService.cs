using System.Runtime.InteropServices;
using CodingTask.Data.Entities;
using CodingTask.Data.Infrastructure;

namespace CodingTask.Shared.Services;

public sealed class HostConfigurationService
{
    private readonly IUnitOfWork uow;

    public HostConfigurationService(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<HostConfiguration> GetOrCreateAsync()
    {
        string manufacturer = "Intel";
        string model = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") ?? "Unknown";
        int physicalCores = Environment.ProcessorCount / 2;
        int logicalThreads = Environment.ProcessorCount;

        var cpuModel = await this.uow.CpuModels.GetExistingAsync(manufacturer, model).ConfigureAwait(false);
        if (cpuModel == null)
        {
            cpuModel = new CpuModel
            {
                Manufacturer = manufacturer,
                Model = model,
                PhysicalCoreCount = physicalCores,
                LogicalThreadCount = logicalThreads,
            };
            await this.uow.CpuModels.AddAsync(cpuModel).ConfigureAwait(false);
            await this.uow.SaveChangesAsync().ConfigureAwait(false);
        }

        string osName = RuntimeInformation.OSDescription;
        string osVersion = Environment.OSVersion.Version.ToString();

        var os = await this.uow.OperatingSystems.GetExistingAsync(osName, osVersion).ConfigureAwait(false);
        if (os == null)
        {
            os = new CodingTask.Data.Entities.OperatingSystem
            {
                Name = osName,
                Version = osVersion,
            };
            await this.uow.OperatingSystems.AddAsync(os).ConfigureAwait(false);
            await this.uow.SaveChangesAsync().ConfigureAwait(false);
        }

        var hostConfig = await this.uow.HostConfigurations.GetExistingAsync(cpuModel.Id, 16, os.Id).ConfigureAwait(false);
        if (hostConfig == null)
        {
            hostConfig = new HostConfiguration
            {
                CpuModelId = cpuModel.Id,
                RamGb = 16,
                OperatingSystemId = os.Id,
            };
            await this.uow.HostConfigurations.AddAsync(hostConfig).ConfigureAwait(false);
            await this.uow.SaveChangesAsync().ConfigureAwait(false);
        }

        return hostConfig;
    }
}