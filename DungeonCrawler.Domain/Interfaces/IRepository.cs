
namespace DungeonCrawler.Domain.Interfaces;

public interface IRepository<TEntity, TId> where TEntity: class
{
    Task<TEntity?> GetByIdAsync(TId id);
    Task AddAsync(TEntity entity);
    Task ReplaceAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}
