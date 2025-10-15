using Labb2_DungeonCrawler.LevelElements;
namespace Labb2_DungeonCrawler.Core;

public class Renderer
{
    public void RenderLevel(List<LevelElement> elements)
    {
        ProcessCharacterRendering(elements);
        ProcessWallRendering(elements);
        Console.ResetColor();
    }
    private void Draw(LevelElement element, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.SetCursorPosition(element.Position.XPos, element.Position.YPos);
        Console.Write(element.RepresentationAsChar);
    }
    public void ClearPosition(Position position)
    {
        Console.SetCursorPosition(position.XPos, position.YPos);
        Console.Write(' ');
    } 
    
    private void ProcessCharacterRendering(List<LevelElement> element)
    {
        foreach (var character in element.OfType<Character>())
        {
            if (character.isVisible)
            {
                LevelElement? charOnPosition = element.FirstOrDefault(element => element.Position == character.PreviousPosition);
                {
                    if (charOnPosition is null)
                    {
                        ClearPosition(character.PreviousPosition);
                    }
                }
                Draw(character, character.Color);
            }
            else if (!character.isVisible)
            {
                ClearPosition(character.Position);
                ClearPosition(character.PreviousPosition);
            }
        }
    }
    private void ProcessWallRendering(List<LevelElement> elements)
    {
        foreach (var wall in elements.OfType<Wall>())
        {

            if (wall.isVisible)
            {
                Draw(wall, ConsoleColor.DarkYellow);
            }
            else if(wall.hasBeenSeen && !wall.isVisible)
            {
                Draw(wall, ConsoleColor.DarkGray);
            }
        }
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
    public void DisplayTitleScreen(StartScreenOption selection)
    {
        int height = 16;
        int width = 32;
        int startX = 0;
        int startY = 0;

        string titleText = "THE DEPTHS";
        string startSelectionText = "Start game";
        string quitGameText = "Run away";


        Console.ForegroundColor = ConsoleColor.DarkRed;

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

        // Write all text centered but with some offets 
        int titleYOffset = 3;
        int titleX = startX + (width - titleText.Length) / 2;
        int titleY = (startY + height / 2) - titleYOffset;
        Console.SetCursorPosition(titleX, titleY);
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write(titleText);

        int startGameX = startX + (width - startSelectionText.Length) / 2;
        int startGameY = startY + height / 2;
        Console.SetCursorPosition(startGameX, startGameY);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(startSelectionText);

        int quitGameYOffset = 1;
        int quitGameXOffset = 1;
        int quitGameX = (startX + (width - quitGameText.Length) - quitGameXOffset) / 2;
        int quitGameY = startY + quitGameYOffset + height / 2;
        Console.SetCursorPosition(quitGameX, quitGameY);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(quitGameText);
        Console.ResetColor();



        if (selection == StartScreenOption.Start)
        {
            Console.SetCursorPosition(startGameX - 3, startGameY);
            Console.Write('=');
            Console.SetCursorPosition(startGameX - 2, startGameY);
            Console.Write('>');
            Console.SetCursorPosition(quitGameX - 3, quitGameY);
            Console.Write(' ');
            Console.SetCursorPosition(quitGameX - 2, quitGameY);
            Console.Write(' ');
        }
        else
        {
            Console.SetCursorPosition(startGameX - 3, startGameY);
            Console.Write(' ');
            Console.SetCursorPosition(startGameX - 2, startGameY);
            Console.Write(' ');
            Console.SetCursorPosition(quitGameX - 3, quitGameY);
            Console.Write('=');
            Console.SetCursorPosition(quitGameX - 2, quitGameY);
            Console.Write('>');
        }

        Console.SetCursorPosition(0, height);
        Console.WriteLine();
    }

    public void RenderUIStats(Character character, int turn, int height, int width, int startX, int startY)
    {
        DrawUIBox(height, width, startX, startY);
        string UITitle = "STATS";
        Console.SetCursorPosition(startX+1, startY+1);
        Console.Write(UITitle);
        Console.SetCursorPosition(startX+1, startY+2);
        Console.Write($"HP: {character.HitPoints}/100");
        Console.SetCursorPosition(startX+1, startY+3);
        Console.Write($"Turn: {turn}");

    }

    public void DrawInstructions(int xCoord, int yCoord)
    {
        Console.SetCursorPosition(xCoord, yCoord);
        Console.WriteLine("Use ASDW or the Arrow keys to move. Any other input will skip your turn.");
    }

    public void DrawDebugValues(bool debug, int xCoord, int yCoord, object Object)
    {
        if (debug)
        {
            Console.SetCursorPosition(xCoord, yCoord);
            Console.WriteLine($"{Object}");
        }
    }

    public void DrawUIBox(int height, int width, int startX, int startY)
    {
        // TOP
        Console.SetCursorPosition(startX, startY);
        Console.Write("+");
        for (int i = 1; i < width - 1; i++)
            Console.Write("-");
        Console.Write("+");

        // BOTH SIDES
        for (int i = 1; i < height - 1; i++)
        {
            Console.SetCursorPosition(startX, startY + i);
            Console.Write("|");
            Console.SetCursorPosition(startX + width - 1, startY + i);
            Console.Write("|");
        }

        // BOTTOM
        Console.SetCursorPosition(startX, startY + height - 1);
        Console.Write("+");
        for (int i = 1; i < width - 1; i++)
            Console.Write("-");
        Console.Write("+");
    }
    public enum StartScreenOption
    {
        Start,
        Quit
    }
}
