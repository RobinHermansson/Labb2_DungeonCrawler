using DungeonCrawler.Domain.Entities;

namespace DungeonCrawler.Domain.Interfaces;

public interface IEnemyRepository : IRepository<Enemy, Guid>
{
}
