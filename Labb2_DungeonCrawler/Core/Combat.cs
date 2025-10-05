using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.LevelElements;

namespace Labb2_DungeonCrawler.Core
{
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
            Console.WriteLine($"A COMBAT HAS STARTED BETWEEN {Aggressor} & {Defender}");
            Console.WriteLine($"{Aggressor.Name} attacks {Defender.Name}");
            Aggressor.Attack(Defender);
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
}
