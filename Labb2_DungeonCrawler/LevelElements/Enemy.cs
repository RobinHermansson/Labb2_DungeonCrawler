namespace Labb2_DungeonCrawler.LevelElements;

public abstract class Enemy : Character
{
    public Enemy(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {

    }

    public abstract void Update();
}

