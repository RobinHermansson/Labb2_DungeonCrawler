using DungeonCrawler.Infrastructure.Repositories.Mongo;
using DungeonCrawler.Domain.Entities;
using MongoDB.Driver;
using DungeonCrawler.Domain.Interfaces;

namespace DungeonCrawler.Infrastructure.Repositories;

public class SaveGameRepository : MongoRepository<SaveGame, Guid>, ISaveGameRepository
{
    public SaveGameRepository(IMongoDatabase database) : base(database, "SavedGames")
    {
    }

    public async Task CreateSaveGameAsync(GameState gameState, int slotNumber)
    {
        var saveGame = SaveGame.FromGameState(gameState, slotNumber, gameState.Player?.Name ?? "Player");
        await _collection.InsertOneAsync(saveGame);
    }

    public async Task<SaveGame?> GetBySlotNumberAsync(int number)
    {
        var filter = Builders<SaveGame>.Filter.Eq(x => x.SlotNumber, number);
        return await _collection.Find(filter).FirstOrDefaultAsync() ;
    }

    public async Task SaveToSlotNumberAsync(SaveGame entity, int number)
    {
        var filter = Builders<SaveGame>.Filter.Eq(x => x.SlotNumber, number);
        var options = new ReplaceOptions { IsUpsert = true };
        await _collection.ReplaceOneAsync(filter, entity, options);

    }
}
