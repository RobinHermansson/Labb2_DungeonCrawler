using Labb2_DungeonCrawler.Features;
namespace Labb2_DungeonCrawler.LevelElements
{
    public interface IFighter
    {
        string Name { get; set; }
        int HitPoints { get; set; }
        List<Dice> AttackDice { get; }
        List<Dice> DefenceDice { get; }
        int AttackModifier {get;}
        int DefenceModifier { get; }


        bool Attack(IFighter target);
        void TakeDamage(int damage);
        bool IsAlive();


    }
}
