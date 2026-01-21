using DungeonCrawler.Domain.ValueObjects;

namespace DungeonCrawler.Domain.Entities;

public abstract class Enemy : Character
{
    public Enemy(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {

    }

    public abstract void Update();
}

