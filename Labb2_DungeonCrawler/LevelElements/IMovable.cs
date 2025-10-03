using Labb2_DungeonCrawler.Core;

namespace Labb2_DungeonCrawler.LevelElements;

public interface IMovable
{
    public bool AttemptMove(Position position, GameState gameState);
    public void MoveTo(Position position);
    public void MoveMe(Direction direction);
}
