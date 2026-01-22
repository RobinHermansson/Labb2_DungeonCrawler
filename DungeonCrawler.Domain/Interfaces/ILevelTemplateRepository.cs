using DungeonCrawler.Domain.Entities;

namespace DungeonCrawler.Domain.Interfaces;

public interface ILevelTemplateRepository
{
    Task<List<LevelTemplate>> GetAllAsync();
    Task<LevelTemplate?> GetByIdAsync();
    Task<LevelTemplate?> GetByLevelNumberAsync(int levelNumber);
    Task SaveAsync(LevelTemplate template);
    Task Exists(LevelTemplate template);
}
