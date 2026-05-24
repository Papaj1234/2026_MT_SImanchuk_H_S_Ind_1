using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface ICpuModelRepository : IRepository<CpuModel>
{
    Task<CpuModel?> GetExistingAsync(string manufacturer, string model);
}