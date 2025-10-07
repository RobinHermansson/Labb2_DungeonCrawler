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
}
