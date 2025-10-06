using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.Utilities;
namespace Labb2_DungeonCrawler.LevelElements;


public class Player : LevelElement, IMovable, IFighter
{
    public string Name { get; set; } = "Player";
    public int HitPoints { get; set; } = 100;
    public int AttackDiceCount { get; set; } = 2;
    public int DefenceDiceCount { get; set; } = 2;
    public int AttackModifier { get; } = 3;
    public int DefenceModifier { get; } = 3;

    public List<Dice> AttackDice { get; } = new List<Dice>();
    public List<Dice> DefenceDice { get; } = new List<Dice>();
    public GameState GameState { get; set; }
    
    public Player(string name, Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
        Name = name;
        VisionRange = 5;
        for (int i = 0; i < AttackDiceCount; i++)
        {
            AttackDice.Add(new Dice());
        }
        for (int i = 0; i < DefenceDiceCount; i++)
        {
            DefenceDice.Add(new Dice());
        }
    }
    public void Update()
    {
    }

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

    }

    public void CheckSurrounding(List<LevelElement> surroundingElements)
    {
        
        foreach (LevelElement element in surroundingElements)
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
    public Position MovementHandler(ConsoleKeyInfo input)
    {
        switch (input.Key)
        {
            case ConsoleKey.W:
                return DirectionTransformer.GetPositionDelta(Direction.Up) + this.Position;
            case ConsoleKey.S:
                return DirectionTransformer.GetPositionDelta(Direction.Down) + this.Position;
            case ConsoleKey.A:
                return DirectionTransformer.GetPositionDelta(Direction.Left) + this.Position;
            case ConsoleKey.D:
                return DirectionTransformer.GetPositionDelta(Direction.Right) + this.Position;
            case ConsoleKey.UpArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Up) + this.Position;
            case ConsoleKey.DownArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Down) + this.Position;
            case ConsoleKey.LeftArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Left) + this.Position;
            case ConsoleKey.RightArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Right) + this.Position;
            default:
                throw new ArgumentException("Could not handle the input key.");
        }

    }
    public bool Attack(IFighter target)
    {
        int attackRollTotal = 0;
        int defenceRollTotal = 0;
        foreach(Dice dice in this.AttackDice)
        {
            int roll = dice.Roll();
            attackRollTotal += roll;
            
        }
        Console.WriteLine($"{this.Name} rolled his {this.AttackDiceCount}d6+{this.AttackModifier}. {this.Name}'s Attacktotal is: {attackRollTotal}+{this.AttackModifier} ({attackRollTotal + this.AttackModifier})");

        foreach(Dice dice in target.DefenceDice)
        {
            int roll = dice.Roll();
            defenceRollTotal += roll;
        }
        Console.WriteLine($"{target.Name} rolled his {this.DefenceDiceCount }d6+{target.DefenceModifier}. {target.Name}'s Defence total is: {defenceRollTotal}+{target.DefenceModifier} ({defenceRollTotal + target.DefenceModifier})");
        if ((attackRollTotal + AttackModifier) > (defenceRollTotal + target.DefenceModifier))
        {
            int totalDamageTaken = (attackRollTotal + this.AttackModifier) - (defenceRollTotal + target.DefenceModifier); 
            Console.WriteLine($"{target.Name} is about to take {totalDamageTaken}!");
            target.TakeDamage(totalDamageTaken);
            return true;
        }
        else
        {
            Console.WriteLine($"{this.Name} misses {target.Name}");
            return false;
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

    
}
