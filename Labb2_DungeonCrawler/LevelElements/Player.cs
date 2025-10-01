using Labb2_DungeonCrawler.Features;
namespace Labb2_DungeonCrawler.LevelElements
{
    public class Player : LevelElement
    {
        public string Name { get; } = "Player";
        public int HitPoints { get; } = 100;
        public int AttackDiceCount { get; }
        public int DefenceDiceCount { get; }
        public int AttackModifier { get; }
        public int DefenceModifiier { get; }

        public Dice AttackDice { get; }
        public Dice DefenceDice { get; }

        public void Update()
        {
        }
        public Player(string name, Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
        {
            Name = name;
        }
    }
}
