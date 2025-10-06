using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.LevelElements;

namespace Labb2_DungeonCrawler.Core;

public class Combat
{
    public IFighter Aggressor { get; set; }
    public IFighter Defender { get; set; }


    public Combat(IFighter aggressor, IFighter defender)
    {
        Aggressor = aggressor;
        Defender = defender;
    }

    public bool StartCombat()
    {
        Console.Clear();
        Console.WriteLine($"A COMBAT HAS STARTED BETWEEN {Aggressor.Name} & {Defender.Name}");
        while (true)
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            if (!Aggressor.IsAlive())
            {
                Console.WriteLine($"{Aggressor.Name} has died!");
                Console.WriteLine("Press ENTER key to continue.");
                Console.ReadLine();
                Console.Clear();
                break;
            }
            Console.WriteLine($"{Aggressor.Name} attacks {Defender.Name}. {Defender.Name} before the attack has {Defender.HitPoints} left!");
            Aggressor.Attack(Defender);
            if (!Defender.IsAlive())
            {
                Console.WriteLine($"{Defender.Name} has died!");
                Console.WriteLine("Press ENTER key to continue.");
                Console.ReadLine();
                Console.Clear();
                
                break;
            }
            Console.WriteLine($"{Defender.Name} attacks {Aggressor.Name}. {Aggressor.Name} before the attack has {Aggressor.HitPoints}");
            Defender.Attack(Aggressor);
            

        }
        return true;
    }

    public int RollDice(List<Dice> diceToRoll)
{
    int sum = 0;
    foreach (Dice dice in diceToRoll)
    {
        Random rand = new Random();
        rand.Next(1, 7);
    }
    return sum;
}
}
