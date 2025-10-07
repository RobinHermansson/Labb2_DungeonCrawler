using Labb2_DungeonCrawler.Features;

namespace Labb2_DungeonCrawler.LevelElements;

public abstract class Enemy : Character
{
    public Enemy(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {

    }

    public abstract void Update();
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

