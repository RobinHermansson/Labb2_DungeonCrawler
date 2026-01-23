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

    public async Task<bool> ExistsAsync(LevelTemplate template)
    {
        var filter = Builders<LevelTemplate>.Filter.Eq(x => x.Id, template.Id);
        return await _collection.Find(filter).AnyAsync();
    }

    public async Task<LevelTemplate?> GetByIdAsync(Guid id)
    {
        var filter = Builders<LevelTemplate>.Filter.Eq(x => x.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<LevelTemplate?> GetByLevelNumberAsync(int levelNumber)
    {
        var filter = Builders<LevelTemplate>.Filter.Eq(x => x.LevelNumber, levelNumber);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task SaveAsync(LevelTemplate template)
    {
        // Upsert: update if exists, insert if not
        var filter = Builders<LevelTemplate>.Filter.Eq(x => x.LevelNumber, template.LevelNumber);
        var options = new ReplaceOptions { IsUpsert = true };
        await _collection.ReplaceOneAsync(filter, template, options); 
    }

    /* Implemented through base...?
    public async Task<List<LevelTemplate>> GetAllAsync()
    {
        var filter = Builders<LevelTemplate>.Filter.Empty;
        return await _collection.Find(filter).ToListAsync();
    
    }
    */
}
