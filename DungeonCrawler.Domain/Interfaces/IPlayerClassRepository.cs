using DungeonCrawler.Domain.Entities;
namespace DungeonCrawler.Domain.Interfaces;
public interface IPlayerClassRepository : IRepository<PlayerClass, Guid>
{
    Task<PlayerClass?> GetByNameAsync(string name);
}