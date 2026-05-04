using Marketplace.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext Context;

    public Repository(AppDbContext context) => Context = context;

    public async Task<T?> GetByIdAsync(Guid id) => 
        await Context.Set<T>().FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await Context.Set<T>().ToListAsync();

    public async Task AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await SaveAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            Context.Set<T>().Remove(entity);
            await SaveAsync();
        }
    }

    public async Task SaveAsync() =>
        await Context.SaveChangesAsync();
}

