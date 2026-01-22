using DungeonCrawler.Domain.Interfaces;
using MongoDB.Driver;

namespace DungeonCrawler.Infrastructure.Repositories.Mongo;

public abstract class MongoRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IHasId<TId>
{
    protected readonly IMongoCollection<TEntity> _collection;
    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<TEntity>(collectionName);
        
    }
    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    public async Task AddAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
    }


    public async Task RemoveAsync(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task ReplaceAsync(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<bool> ExistsAsync(TId id)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
        return await _collection.Find(filter).AnyAsync();
    }


    public async Task<long> CountAsync()
    {
        return await _collection.CountDocumentsAsync(_ => true);
    }
}
