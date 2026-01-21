using DungeonCrawler.Domain.ValueObjects;
using DungeonCrawler.Domain.Utilities;

namespace DungeonCrawler.Domain.Entities;

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
        PreviousPosition = new Position(Position.XPos, Position.YPos);
        Position = position;
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

    public virtual void CheckSurrounding(List<LevelElement> allElements)
    {
        var nearbyElements = allElements.Where(element => 
            Math.Abs(element.Position.XPos - Position.XPos) <= VisionRange &&
            Math.Abs(element.Position.YPos - Position.YPos) <= VisionRange
        );
        /*
        foreach (var element in allElements)
        {
            element.IsVisible = false;   
        }
        */
        List<LevelElement> visibleElements = new List<LevelElement>();
        foreach (LevelElement element in nearbyElements)
        {
            var distance = CalculateDistance.Between(Position, element.Position);
            if (distance < VisionRange)
            {
                if (element is Wall)
                {
                    element.HasBeenSeen = true;
                }
                element.IsVisible = true;
                if (!visibleElements.Contains(element))
                {
                    visibleElements.Add(element);
                }
            }
        }

        foreach (var element in allElements)
        {
            if (!visibleElements.Contains(element))
            {
                element.IsVisible = false;
            }
        }
    }
    public override void UpdateColor()
    {
        if (IsVisible)
        {
            // Wall is currently visible - show in dark yellow
            Color = ConsoleColor.Yellow;
        }
        else if (HasBeenSeen && !IsVisible)
        {
            // Wall has been seen before but not currently visible - show in gray
            Color = ConsoleColor.Yellow;
            //Console.WriteLine("Is now visible and color is gray");
        }
        else
        {
            // Wall has never been seen - could be invisible or a specific color
            Color = ConsoleColor.Black; // Or whatever color for unseen walls
        }
    }
}
