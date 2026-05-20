using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data.Repositories;

public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly AppDbContext context;
    private readonly DbSet<T> dbSet;

    public Repository(AppDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }

    protected AppDbContext Context => this.context;

    protected DbSet<T> DbSet => this.dbSet;

    public async Task<T?> GetByIdAsync(int id) =>
        await this.dbSet.FindAsync(id).ConfigureAwait(false);

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await this.dbSet.ToListAsync().ConfigureAwait(false);

    public async Task AddAsync(T entity) =>
        await this.dbSet.AddAsync(entity).ConfigureAwait(false);

    public Task UpdateAsync(T entity)
    {
        this.dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await this.GetByIdAsync(id).ConfigureAwait(false);
        if (entity != null)
        {
            this.dbSet.Remove(entity);
        }
    }
}