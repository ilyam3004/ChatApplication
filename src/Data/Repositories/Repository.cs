using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly ApplicationDbContext DbContext;

    public Repository(ApplicationDbContext context)
    {
        DbContext = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbContext.Set<TEntity>().AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task Remove(TEntity entity)
    { 
        DbContext.Set<TEntity>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task<List<TEntity>> GetAll()
        => await DbContext.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> GetByIdAsync(Guid id)
        => await DbContext.Set<TEntity>().FindAsync(id);
} 