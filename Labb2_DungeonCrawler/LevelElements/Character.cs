using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.Utilities;
namespace Labb2_DungeonCrawler.LevelElements;

public abstract class Character : LevelElement
{
    public string Name { get; set; }
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
    public bool Attack(Character target)
    {
        int attackRollTotal = 0;
        int defenceRollTotal = 0;
        foreach (Dice dice in this.AttackDice)
        {
            int roll = dice.Roll();
            attackRollTotal += roll;

        }
        Console.WriteLine($"{this.Name} rolled his {this.AttackDiceCount}d6+{this.AttackModifier}. {this.Name}'s Attacktotal is: {attackRollTotal}+{this.AttackModifier} ({attackRollTotal + this.AttackModifier})");

        foreach (Dice dice in target.DefenceDice)
        {
            int roll = dice.Roll();
            defenceRollTotal += roll;
        }
        Console.WriteLine($"{target.Name} rolled his {this.DefenceDiceCount}d6+{target.DefenceModifier}. {target.Name}'s Defence total is: {defenceRollTotal}+{target.DefenceModifier} ({defenceRollTotal + target.DefenceModifier})");
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
}
