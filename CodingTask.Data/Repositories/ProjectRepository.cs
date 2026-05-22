using CodingTask.Data.Entities;
using CodingTask.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<Project?> GetByFolderPathAsync(string folderPath) => await this.DbSet.FirstOrDefaultAsync(p => p.FolderPath == folderPath).ConfigureAwait(false);
}