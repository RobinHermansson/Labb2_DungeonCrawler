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

    public virtual void CheckSurrounding(List<LevelElement> allElements)
    {
        var nearbyElements = allElements.Where(element => 
            Math.Abs(element.Position.XPos - this.Position.XPos) <= VisionRange &&
            Math.Abs(element.Position.YPos - this.Position.YPos) <= VisionRange
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
            var distance = CalculateDistance.Between(this.Position, element.Position);
            if (distance < this.VisionRange)
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
        if (this.IsVisible)
        {
            // Wall is currently visible - show in dark yellow
            this.Color = ConsoleColor.Yellow;
        }
        else if (this.HasBeenSeen && !this.IsVisible)
        {
            // Wall has been seen before but not currently visible - show in gray
            this.Color = ConsoleColor.Yellow;
            //Console.WriteLine("Is now visible and color is gray");
        }
        else
        {
            // Wall has never been seen - could be invisible or a specific color
            this.Color = ConsoleColor.Black; // Or whatever color for unseen walls
        }
    }
}
