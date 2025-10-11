using Labb2_DungeonCrawler.LevelElements;
namespace Labb2_DungeonCrawler.CharacterClasses;

public interface ICharacterClass
{
    string ClassName { get; }
    int BonusHealth { get; }
    int ShieldHealth { get; }
    int AttackBonus { get; }
    int DefenseBonus { get; }
    
    bool UseSpecialAbility(Character user, Character target);
    string GetClassDescription();
}
