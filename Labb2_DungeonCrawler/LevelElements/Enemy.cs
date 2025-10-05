using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.Utilities;

namespace Labb2_DungeonCrawler.LevelElements;

public abstract class Enemy : LevelElement, IMovable, IFighter
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
    public Enemy(Position pos, char representation, ConsoleColor color)  : base(pos, representation, color)
    {
         
    }

    public abstract void Update();
    public bool Attack(IFighter target)
    {
        int attackRollTotal = 0;
        int defenceRollTotal = 0;
        foreach(Dice dice in AttackDice)
        {
            attackRollTotal += dice.Roll();
            Console.WriteLine($"{Name} rolls, and the attacktotal is: {attackRollTotal}");
        }
        foreach(Dice dice in target.DefenceDice)
        {
            defenceRollTotal += dice.Roll();
            Console.WriteLine($"{target.Name} rolls, and the defencetotal is: {defenceRollTotal}");
        }
        if ((attackRollTotal + AttackModifier) > (defenceRollTotal + target.DefenceModifier))
        {
            int totalDamageTaken = (attackRollTotal + AttackModifier) - (defenceRollTotal + target.DefenceModifier); 
            Console.WriteLine($"{target.Name} is about to take {totalDamageTaken}!");
            target.TakeDamage(totalDamageTaken);
            return true;
        }
        else
        {
            return false;
        }
            
        
    }
    
    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
    }


    public bool IsAlive()
    {
        throw new NotImplementedException();
    }
}
