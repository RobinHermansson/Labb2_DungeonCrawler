using Labb2_DungeonCrawler.LevelElements;

namespace Labb2_DungeonCrawler.Core;

public class CombatRenderer
{
    private readonly int _logSize = 10;
    private readonly List<string> _combatLog = new List<string>();
    
    public void RenderCombatScreen(Character aggressor, Character defender)
    {

        Console.Clear();
    
        // Draw combat screen border
        DrawBorder(0, 0, Console.WindowWidth, Console.WindowHeight);
        
        // Draw stats sections
        RenderStats(aggressor, 2, 2, "ATTACKER");
        RenderStats(defender, Console.WindowWidth / 2 + 2, 2, "DEFENDER");
        
        // Draw combat log
        RenderCombatLog();
        
        // Draw instructions
        Console.SetCursorPosition(2, Console.WindowHeight - 2);
        Console.Write("Press ENTER to continue combat, ESC to attempt escape");

    }
    public void RenderStats(Character character, int x, int y, string title )
    {
        int width = Console.WindowWidth / 2 - 4;
        int height = 7;
        
        // Draw stats box
        DrawBox(x, y, width, height);
        
        // Set title color based on character type
        Console.ForegroundColor = title == "DEFENDER" ? ConsoleColor.Cyan : ConsoleColor.Red;
        Console.SetCursorPosition(x + (width - title.Length) / 2, y);
        Console.Write(title);
        Console.ResetColor();
        
        // Display stats
        Console.SetCursorPosition(x + 2, y + 1);
        Console.Write($"Name: {character.Name}");
        
        Console.SetCursorPosition(x + 2, y + 2);
        Console.Write($"HP: ");
        Console.Write($"{character.HitPoints}");
        
        Console.SetCursorPosition(x + 2, y + 3);
        Console.Write($"Attack: {character.AttackDiceCount}d6+{character.AttackModifier}");
        
        Console.SetCursorPosition(x + 2, y + 4);
        Console.Write($"Defense: {character.DefenceDiceCount}d6+{character.DefenceModifier}");


    }
    public void AddLogEntry(string entry, bool isHit = false, bool isMiss = false)
    {
        // Format the log entry with a timestamp and appropriate color indicators
        string formattedEntry = entry;

        if (isHit)
        {
            formattedEntry = "✓ " + formattedEntry;
        }
        else if (isMiss)
        {
            formattedEntry = "X " + formattedEntry;
        }
        
        _combatLog.Add(formattedEntry);
        
        // Keep log at manageable size
        if (_combatLog.Count > 100) _combatLog.RemoveAt(0);
    }
    
    
    private void RenderCombatLog()
    {
        int logX = 2;
        int logY = 10;
        int logWidth = Console.WindowWidth - 4;
        int logHeight = Console.WindowHeight - 14;
        
        // Draw log box
        DrawBox(logX, logY, logWidth, logHeight);
        
        // Draw log title
        Console.SetCursorPosition(logX + (logWidth - 10) / 2, logY);
        Console.Write("COMBAT LOG");
        
        // Display log entries
        int startIndex = Math.Max(0, _combatLog.Count - logHeight + 2);
        for (int i = 0; i < Math.Min(_combatLog.Count, logHeight - 2); i++)
        {
            Console.SetCursorPosition(logX + 2, logY + i + 1);
            if (_combatLog[startIndex + i].Contains('✓'))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(_combatLog[startIndex + i]);
                Console.ResetColor();
            }
            else if (_combatLog[startIndex + i].Contains("X "))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(_combatLog[startIndex + i]);
                Console.ResetColor();
            }
            else
            {
                Console.Write(_combatLog[startIndex + i]);
            }

                
        }
    }
    private void DrawBox(int x, int y, int width, int height)
    {
        // Draw top border
        Console.SetCursorPosition(x, y);
        Console.Write("┌");
        for (int i = 1; i < width - 1; i++) Console.Write("─");
        Console.Write("┐");
        
        // Draw sides
        for (int i = 1; i < height - 1; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write("│");
            Console.SetCursorPosition(x + width - 1, y + i);
            Console.Write("│");
        }
        
        // Draw bottom border
        Console.SetCursorPosition(x, y + height - 1);
        Console.Write("└");
        for (int i = 1; i < width - 1; i++) Console.Write("─");
        Console.Write("┘");
    }

    private void DrawBorder(int x, int y, int width, int height)
    {
        DrawBox(x, y, width, height);
    }


}
