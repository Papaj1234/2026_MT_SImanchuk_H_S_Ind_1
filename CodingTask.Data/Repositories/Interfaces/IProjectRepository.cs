using CodingTask.Data.Entities;

namespace CodingTask.Data.Repositories.Interfaces;

public interface IProjectRepository : IRepository<Project>
{
    Task<Project?> GetByFolderPathAsync(string folderPath);
}