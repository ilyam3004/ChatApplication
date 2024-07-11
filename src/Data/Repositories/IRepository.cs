namespace Data.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<List<TEntity>> GetAll();
    Task<TEntity?> GetByIdAsync(Guid id);
}