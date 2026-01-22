using DungeonCrawler.Domain.Entities;

namespace DungeonCrawler.Domain.Interfaces;


public interface IPlayerRepository : IRepository<Player, Guid>
{
}
