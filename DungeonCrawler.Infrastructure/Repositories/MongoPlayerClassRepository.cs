using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using MongoDB.Driver;

namespace DungeonCrawler.Infrastructure.Repositories.Mongo;

public class MongoPlayerClassRepository : MongoRepository<PlayerClass, Guid>, IPlayerClassRepository
{
    public MongoPlayerClassRepository(IMongoDatabase database) 
        : base(database, "playerClasses")
    {
    }

    public async Task<PlayerClass?> GetByNameAsync(string name)
    {
        var filter = Builders<PlayerClass>.Filter.Eq(x => x.Name, name);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

}