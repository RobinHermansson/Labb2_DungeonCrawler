using Labb2_DungeonCrawler.App.LevelElements;
using Labb2_DungeonCrawler.App.Utilities;

namespace Labb2_DungeonCrawler.App.Core;

public class Renderer
{
    public void RegisterElement(LevelElement element)
    {
        element.PositionChanged += HandlePositionChanged;
        element.VisibilityChanged += HandleVisiblityChanged;
        // Make the initial draw of the element upon registration.
        Draw(element);
    }

    public void UnsubscribeElement(LevelElement element)
    {
        element.PositionChanged -= HandlePositionChanged;
        element.VisibilityChanged -= HandleVisiblityChanged;
        ClearPosition(element.Position);
    }

    public void HandlePositionChanged(object sender, Position position)
    {
        LevelElement element = (LevelElement)sender;

        Draw(element);
    }

    public void HandleVisiblityChanged(object sender, bool newState)
    {
        LevelElement element = (LevelElement)sender;
        ClearPosition(element.Position);
        Draw(element);
    }

    public void RenderLevel(List<LevelElement> elements)
    {
        foreach (LevelElement element in elements)
        {
            RegisterElement(element);
        }
    }
    private void Draw(LevelElement element)
    {

        Console.ForegroundColor = element.Color;
        Console.SetCursorPosition(element.Position.XPos, element.Position.YPos);
        Console.Write(element.RepresentationAsChar);
        ClearPosition(element.PreviousPosition);
        Console.ResetColor();

    }
    public void DebugDraw(DebugAssistant debugger)
    {
        Console.ForegroundColor = debugger.Color;
        Console.SetCursorPosition(debugger.Position.XPos, debugger.Position.YPos);
        Console.Write(debugger.CurrentRepresentation);
        Console.SetCursorPosition(debugger.PreviousObjectPosition.XPos, debugger.PreviousObjectPosition.YPos);
        Console.ForegroundColor = debugger.PreviousObjectPositionColor;
        if (debugger.PreviousObjectPositionRepresentation == 'O')
        {
            Console.Write(' ');
        }
        else
        {
            Console.Write(debugger.PreviousObjectPositionRepresentation);
        }
        Console.ResetColor();

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
        int titleY = startY + height / 2 - titleYOffset;
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
        Console.ForegroundColor = ConsoleColor.DarkRed;
        DrawUIBox(height, width, startX, startY);
        string UITitle = "STATS";
        Console.SetCursorPosition(startX + 1, startY + 1);
        Console.Write(UITitle);
        Console.SetCursorPosition(startX + 1, startY + 2);
        Console.Write($"HP: {character.HitPoints}/100");
        Console.SetCursorPosition(startX + 1, startY + 3);
        Console.Write($"Turn: {turn}");

    }

    public void DrawInstructions(int xCoord, int yCoord)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
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
        Console.ForegroundColor = ConsoleColor.DarkRed;

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
