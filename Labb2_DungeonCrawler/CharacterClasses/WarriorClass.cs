using Labb2_DungeonCrawler.LevelElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_DungeonCrawler.CharacterClasses
{
    public class WarriorClass : ICharacterClass
    {
        public string ClassName => "Warrior";

        public int BonusHealth => 30;

        public int ShieldHealth => 0;

        public int AttackBonus => 3;

        public int DefenseBonus => 2;

        public string GetClassDescription()
        {
            return "Warriors excel in close combat, with higher health and attack power";
        }

        public bool UseSpecialAbility(Character user, Character target)
        {
            Console.WriteLine($"{user.Name} performs a MIGHTY STRIKE!");
            
            // Double damage on this attack
            //int normalDamage = user.CalculateAttackDamage();
            //int specialDamage = normalDamage * 2;
            
            //target.TakeDamage(specialDamage);
            return true;
        }
    }
}
