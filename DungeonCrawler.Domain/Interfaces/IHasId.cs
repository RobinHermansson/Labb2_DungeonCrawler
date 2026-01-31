namespace DungeonCrawler.Domain.Interfaces;

public interface IHasId<TId>
{
    TId Id { get; }
}
