using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.Utilities;

namespace Labb2_DungeonCrawler.LevelElements;

public abstract class Enemy : LevelElement, IMovable
{
    public string Name { get; }
    public int HitPoints { get; }
    public int AttackDiceCount { get; }
    public int DefenceDiceCount { get; }
    public int AttackModifier { get; }
    public int DefenceModifiier { get; }

    public Dice AttackDice { get; }
    public Dice DefenceDice { get; }

    public bool AttemptMove(Position attempt, GameState gameState)
    {
        return gameState.IsPositionWalkable(attempt) ? true : false;
    }
    public void MoveTo(Position position)
        {
            this.Position = position;
        }
    public void MoveMe(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                Position = DirectionTransformer.GetPositionDelta(Direction.Up) + Position;
                break;
            case Direction.Down:
                Position = DirectionTransformer.GetPositionDelta(Direction.Down) + Position;
                break;
            case Direction.Left:
                Position = DirectionTransformer.GetPositionDelta(Direction.Left) + Position;
                break;
            case Direction.Right:
                Position = DirectionTransformer.GetPositionDelta(Direction.Right) + Position;
                break;
        }
    }
    public Enemy(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
         
    }

    public abstract void Update();
}
