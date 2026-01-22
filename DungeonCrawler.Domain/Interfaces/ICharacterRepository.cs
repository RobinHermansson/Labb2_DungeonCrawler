using DungeonCrawler.Domain.Entities;

namespace DungeonCrawler.Domain.Interfaces;

public interface ICharacterRepository : IRepository<Character, Guid>
{
}
