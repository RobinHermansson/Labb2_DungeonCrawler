using DungeonCrawler.Domain.Entities;

namespace DungeonCrawler.Domain.Interfaces;

public interface ISaveGameRepository : IRepository<SaveGame, Guid>
{
    Task<SaveGame?> GetBySlotNumberAsync(int number);
    Task SaveToSlotNumberAsync(SaveGame entity, int number);
    Task CreateSaveGameAsync(GameState gameState, int number);
}
