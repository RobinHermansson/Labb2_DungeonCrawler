using DungeonCrawler.Domain.Interfaces;

namespace DungeonCrawler.Infrastructure.Mongo;

public class MongoRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IHasId<TId>
{
    public Task AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task ReplaceAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
