using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Infrastructure.Repositories.Mongo;
using MongoDB.Driver;

namespace DungeonCrawler.Infrastructure.Repositories;

public class MongoLevelTemplateRepository : MongoRepository<LevelTemplate, Guid>, ILevelTemplateRepository
{
    public MongoLevelTemplateRepository(IMongoDatabase database) : base(database, "LevelTemplates")
    {
    }

    public async Task Exists(LevelTemplate template)
    {
        throw new NotImplementedException();
    }

    public async Task<LevelTemplate?> GetByIdAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<LevelTemplate?> GetByLevelNumberAsync(int levelNumber)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync(LevelTemplate template)
    {
        // Upsert: update if exists, insert if not
        var filter = Builders<LevelTemplate>.Filter.Eq(x => x.LevelNumber, template.LevelNumber);
        var options = new ReplaceOptions { IsUpsert = true };
        await _collection.ReplaceOneAsync(filter, template, options); 
    }

    public async Task<List<LevelTemplate>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
