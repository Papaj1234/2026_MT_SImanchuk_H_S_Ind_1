using CodingTask.Data.Repositories.Interfaces;

namespace CodingTask.Data.Repositories;

public class OperatingSystemRepository : Repository<Entities.OperatingSystem>, IOperatingSystemRepository
{
    public OperatingSystemRepository(AppDbContext context)
        : base(context)
    {
    }
}