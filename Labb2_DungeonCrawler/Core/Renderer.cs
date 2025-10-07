using Labb2_DungeonCrawler.LevelElements;
namespace Labb2_DungeonCrawler.Core;

public class Renderer
{
    public void RenderLevel(List<LevelElement> elements)
    {
        foreach (var element in elements)
        {
            if (element.isVisible && element is not Wall)
            {
                element.Draw();
            }
            else if (element is Wall && element.isVisible)
            {
                element.Color = ConsoleColor.DarkYellow;
                element.Draw();
            }
            else if (element is Wall && element.hasBeenSeen && !element.isVisible)
            {
                element.Color = ConsoleColor.DarkGray;
                element.Draw();
            }
        }
    }
    public void ClearPosition(Position position)
    {
        Console.SetCursorPosition(position.XPos, position.YPos);
        Console.Write(' ');
    }
    public void DisplayGameOver()
    {
        int height = 10;
        int width = 20;
        int startX = 0;     
        int startY = 0;    
        Console.Clear();
        
        // Draw the top border
        Console.SetCursorPosition(startX, startY);
        Console.Write("╔");
        for (int i = 1; i < width - 1; i++)
            Console.Write("═");
        Console.Write("╗");
        
        // Draw the sides
        for (int i = 1; i < height - 1; i++)
        {
            Console.SetCursorPosition(startX, startY + i);
            Console.Write("║");
            Console.SetCursorPosition(startX + width - 1, startY + i);
            Console.Write("║");
        }
        
        // Draw the bottom border
        Console.SetCursorPosition(startX, startY + height - 1);
        Console.Write("╚");
        for (int i = 1; i < width - 1; i++)
            Console.Write("═");
        Console.Write("╝");
        
        // Write the centered text
        string gameOverText = "Game Over!";
        int textX = startX + (width - gameOverText.Length) / 2;
        int textY = startY + height / 2;
        Console.SetCursorPosition(textX, textY);
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.Write(gameOverText);
        Console.ResetColor();
        Console.SetCursorPosition(0, 10);
        Console.WriteLine(); 
    }
}
