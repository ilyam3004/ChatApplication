namespace Data.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task Update(TEntity entity);
    Task Remove(TEntity entity);
    Task<List<TEntity>> GetAll();
    Task<TEntity?> GetByIdAsync(Guid id);
}