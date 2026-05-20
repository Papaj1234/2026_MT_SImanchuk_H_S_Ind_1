using CodingTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class ProjectRepository : Repository<Project>
{
    public ProjectRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<Project?> GetByFolderPathAsync(string folderPath) => await this.DbSet.FirstOrDefaultAsync(p => p.FolderPath == folderPath).ConfigureAwait(false);
}