using Labb2_DungeonCrawler.Core;

namespace Labb2_DungeonCrawler.LevelElements;

public class Snake: Enemy, IMovable
{
    public Snake(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
         
    }

    public bool AttemptMove()
    {
        return false;
    }

    public override void Update()
    {
        AttemptMove(Position, GameState);
    }
}
