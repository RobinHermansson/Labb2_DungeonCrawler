using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.Utilities;
namespace Labb2_DungeonCrawler.LevelElements;

public abstract class Character : LevelElement
{
    public string Name { get; set; }
    public bool IsPlayer { get; set; } = false;
    public int HitPoints { get; set; }
    public int AttackDiceCount { get; set; }
    public int DefenceDiceCount { get; set; }
    public int AttackModifier { get; set; }
    public int DefenceModifier { get; set; }
    public List<Dice> AttackDice { get; set; }
    public List<Dice> DefenceDice { get; set; }
    public GameState GameState { get; set; }
    public Character(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {

    }

    public bool AttemptMove(Position attempt, GameState gameState)
    {
        return gameState.IsPositionWalkable(attempt) ? true : false;
    }
    public void MoveTo(Position position)
    {
        this.PreviousPosition = new Position(Position.XPos, Position.YPos);
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
    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
    }
    public bool IsAlive()
    {
        return HitPoints > 0;
    }

    public virtual void CheckSurrounding(List<LevelElement> surroundingElements)
    {

        // needed to reset all object's visibility...
        foreach (var element in surroundingElements)
        {
            element.isVisible = false;   
        }
    
        var nearbyElements = surroundingElements.Where(element => 
            Math.Abs(element.Position.XPos - this.Position.XPos) <= VisionRange &&
            Math.Abs(element.Position.YPos - this.Position.YPos) <= VisionRange
        );

        foreach (LevelElement element in nearbyElements)
        {
            var distance = CalculateDistance.Between(this.Position, element.Position);
            if (distance < this.VisionRange)
            {
                if (element is Wall)
                {
                    element.hasBeenSeen = true;
                }
                element.isVisible = true;
            }
            else
            {
                element.isVisible = false;
            }
        }
    }
}
