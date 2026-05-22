using CodingTask.Data.Entities;
using CodingTask.Data.Repositories.Interfaces;

namespace CodingTask.Data.Repositories;

public class CpuModelRepository : Repository<CpuModel>, ICpuModelRepository
{
    public CpuModelRepository(AppDbContext context)
        : base(context)
    {
    }
}