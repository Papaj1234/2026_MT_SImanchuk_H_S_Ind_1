using CodingTask.Data.Entities;
using CodingTask.Data.Infrastructure;
using PerformanceAnalytics.Interfaces;
using OperatingSystem = CodingTask.Data.Entities.OperatingSystem;

namespace PerformanceAnalytics.Loading;

public sealed class AnalyticsDataLoader : IAnalyticsDataLoader
{
    private readonly IUnitOfWork unitOfWork;

    public AnalyticsDataLoader(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<AnalyticsDataSet> LoadAsync()
    {
        var hosts = await this.unitOfWork.HostConfigurations.GetAllAsync().ConfigureAwait(false);
        var cpus = await this.unitOfWork.CpuModels.GetAllAsync().ConfigureAwait(false);
        var systems = await this.unitOfWork.OperatingSystems.GetAllAsync().ConfigureAwait(false);
        var matrices = await this.unitOfWork.MatrixBenchmarks.GetAllAsync().ConfigureAwait(false);
        var scans = await this.unitOfWork.HttpScanRuns.GetAllAsync().ConfigureAwait(false);

        var cpuById = cpus.ToDictionary(c => c.Id);
        var osById = systems.ToDictionary(o => o.Id);

        var labelByHostId = hosts.ToDictionary(
            h => h.Id,
            h => BuildLabels(h, cpuById, osById));

        var matrixSamples = matrices
            .Select(m =>
            {
                var (hostLabel, cpuLabel) = labelByHostId[m.HostConfigurationId];
                return new MatrixSample(
                    m.HostConfigurationId,
                    hostLabel,
                    cpuLabel,
                    m.MatrixType,
                    m.MatrixSize,
                    m.MethodName,
                    m.ElapsedMs);
            })
            .ToList();

        var scanSamples = scans
            .Select(s =>
            {
                var (hostLabel, cpuLabel) = labelByHostId[s.HostConfigurationId];
                return new ScanSample(
                    s.HostConfigurationId,
                    hostLabel,
                    cpuLabel,
                    s.OperationType,
                    s.MaxParallelism,
                    s.ElapsedMs,
                    s.SuccessCount,
                    s.ErrorCount);
            })
            .ToList();

        return new AnalyticsDataSet(matrixSamples, scanSamples);
    }

    private static (string HostLabel, string CpuLabel) BuildLabels(
    HostConfiguration host,
    Dictionary<int, CpuModel> cpuById,
    Dictionary<int, OperatingSystem> osById)
    {
        var cpu = cpuById[host.CpuModelId];
        var os = osById[host.OperatingSystemId];

        var cpuLabel = $"{cpu.Manufacturer} {cpu.Model}";
        var hostLabel = $"{cpuLabel} / {os.Name} {os.Version} / {host.RamGb:0.#} GB";

        return (hostLabel, cpuLabel);
    }
}