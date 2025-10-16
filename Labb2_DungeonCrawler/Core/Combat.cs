using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.LevelElements;

namespace Labb2_DungeonCrawler.Core;

public class Combat
{
    public Character Aggressor { get; set; }
    public Character Defender { get; set; }
    private CombatRenderer _renderer = new CombatRenderer();


    public Combat(Character aggressor, Character defender)
    {
        Aggressor = aggressor;
        Defender = defender;
    }

    public bool StartCombat()
    {
        bool combatActive = true;
        
        _renderer.RenderCombatScreen(Aggressor, Defender);
        _renderer.AddLogEntry($"Combat started between {Aggressor.Name} and {Defender.Name}");
        
        while (combatActive && Aggressor.IsAlive() && Defender.IsAlive())
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape && Aggressor.IsPlayer)
            {
                _renderer.AddLogEntry($"{Aggressor.Name} tries to escape...                      ");
                _renderer.UpdateStatsAndLog(Aggressor, Defender);
                
                if (new Random().Next(2) == 0)
                {
                    _renderer.AddLogEntry("Escape successful!                      ");
                    _renderer.UpdateStatsAndLog(Aggressor, Defender);
                    Console.ReadKey(true);
                    Console.Clear();
                    return false;
                }
                else
                {
                    _renderer.AddLogEntry("Escape failed!                      ");
                    _renderer.UpdateStatsAndLog(Aggressor, Defender);
                }
            }
            
            PerformAttack(Aggressor, Defender);
            _renderer.UpdateStatsAndLog(Aggressor, Defender);
            
            if (!Defender.IsAlive())
            {
                _renderer.AddLogEntry($"{Defender.Name} has been defeated!");
                _renderer.UpdateStatsAndLog(Aggressor, Defender);
                Console.ReadKey(true);
                Console.Clear();
                return true;
            }

            key = Console.ReadKey(true); //Thread.Sleep(2000);
            if (key.Key == ConsoleKey.Escape && Defender.IsPlayer)
            {
                _renderer.AddLogEntry($"{Defender.Name} tries to escape...");
                _renderer.UpdateStatsAndLog(Aggressor, Defender);
                
                if (new Random().Next(2) == 0)
                {
                    _renderer.AddLogEntry("Escape successful!                      ");
                    _renderer.UpdateStatsAndLog(Aggressor, Defender);
                    Console.ReadKey(true);
                    Console.Clear();
                    return false;
                }
                else
                {
                    _renderer.AddLogEntry("Escape failed!                     ");
                    _renderer.UpdateStatsAndLog(Aggressor, Defender);
                }
            }

            
            PerformAttack(Defender, Aggressor);
            _renderer.UpdateStatsAndLog(Aggressor, Defender);
            
            if (!Aggressor.IsAlive())
            {
                _renderer.AddLogEntry($"{Aggressor.Name} has been defeated!                      ");
                _renderer.UpdateStatsAndLog(Aggressor, Defender);
                Console.ReadKey(true);
                Console.Clear();
                return false;
            }
        }
        
        return Aggressor.IsAlive();
    }
    
    private void PerformAttack(Character attacker, Character defender)
    {
        int attackRollTotal = 0;
        int defenceRollTotal = 0;
        
        // Roll attack dice and log results
        string attackRolls = "";
        foreach (Dice dice in attacker.AttackDice)
        {
            int roll = dice.Roll();
            attackRolls += (attackRolls.Length > 0 ? ", " : "") + roll;
            attackRollTotal += roll;
        }
        
        // Roll defense dice and log results
        string defenseRolls = "";
        foreach (Dice dice in defender.DefenceDice)
        {
            int roll = dice.Roll();
            defenseRolls += (defenseRolls.Length > 0 ? ", " : "") + roll;
            defenceRollTotal += roll;
        }
        
        int attackTotal = attackRollTotal + attacker.AttackModifier;
        int defenseTotal = defenceRollTotal + defender.DefenceModifier;
        
        _renderer.AddLogEntry($"{attacker.Name} attacks: [{attackRolls}]+{attacker.AttackModifier} = {attackTotal}");
        _renderer.AddLogEntry($"{defender.Name} defends: [{defenseRolls}]+{defender.DefenceModifier} = {defenseTotal}");
        
        // Determine hit or miss
        if (attackTotal > defenseTotal)
        {
            int damage = attackTotal - defenseTotal;
            _renderer.AddLogEntry($"{attacker.Name} hits for {damage} damage!", isHit: true);
            defender.TakeDamage(damage);
        }
        else
        {
            _renderer.AddLogEntry($"{attacker.Name}'s attack missed!", isMiss: true);
        }
    }
}
