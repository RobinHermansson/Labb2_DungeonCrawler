using Labb2_DungeonCrawler.LevelElements;

namespace Labb2_DungeonCrawler.CharacterClasses;

public class MageClass : ICharacterClass
{
    public string ClassName => "Mage";

    public int BonusHealth => -15;

    public int ShieldHealth => 30;

    public int AttackBonus => 1;

    public int DefenseBonus => 0;

    public string GetClassDescription()
    {
        return "Mages wield powerful magic, dealing consistent damage but having less health. Has a Magic shield.";
    }

    public bool UseSpecialAbility(Character user, Character target)
    {
        // Mage's special: Fireball
        //Console.WriteLine($"{user.Name} casts FIREBALL!");
        
        // Guaranteed hit that bypasses defense
        //Random rand = new Random();
        //int damage = rand.Next(5, 16); // 5-15 damage
        
        //Console.WriteLine($"The spell deals {damage} damage, ignoring defense!");
        //target.TakeDamage(damage, true); // true means bypass defense
        return true;    
    }
}
