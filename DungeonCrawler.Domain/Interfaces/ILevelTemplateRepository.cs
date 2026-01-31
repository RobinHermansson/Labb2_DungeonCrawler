using DungeonCrawler.Domain.Entities;

namespace DungeonCrawler.Domain.Interfaces;

public interface ILevelTemplateRepository
{
    Task<LevelTemplate?> GetByIdAsync(Guid id);
    Task<LevelTemplate?> GetByLevelNumberAsync(int levelNumber);
    Task SaveAsync(LevelTemplate template);
    Task<bool> ExistsAsync(LevelTemplate template);
}
