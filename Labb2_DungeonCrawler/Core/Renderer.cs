using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.ValueObjects;
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

        DrawABox(height, width, startX, startY, '═', '║', '╔', '╗', '╚', '╝');

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

        //Console.Clear();
        int height = Console.WindowHeight;
        int width = Console.WindowWidth;
        int startX = 0;
        int startY = 0;

        string titleText = "THE DEPTHS";
        string startSelectionText = "Start game";
        string quitGameText = "Run away";


        Console.ForegroundColor = ConsoleColor.DarkRed;

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

        Console.SetCursorPosition(0, 0);
        Console.WriteLine();
    }

    public void FillTextInsideBox(char charToWrite, int origboxHeight, int origBoxWidth, int origBoxXpos, int origBoxYpos)
    {
        for (int row = 1; row < origboxHeight - 1; row++)
        {
            for (int col = 1; col < origBoxWidth - 1; col++)
            {
                Console.SetCursorPosition(origBoxXpos + col, origBoxYpos + row);
                Console.Write(charToWrite);
            }
        }
    }
    public void DisplayLoadSaveScreen(LoadSavesScreenOption selection, IEnumerable<SaveGame> savedGames)
    {
        string[] saveGameInfo = new string[4];
        if (savedGames is not null)
        {
            saveGameInfo[0] = "Player: NameOfPlayer";
            saveGameInfo[1] = "Turn: 150";
            saveGameInfo[2] = "Last played: 2024-01-15 14:30";
            saveGameInfo[3] = "Class: Warrior";
        }
        else
        {
            saveGameInfo[0] = "- NO SAVE DATA -";
            saveGameInfo[1] = "- NO SAVE DATA -";
            saveGameInfo[2] = "- NO SAVE DATA -";
            saveGameInfo[3] = "- NO SAVE DATA -";
        }

        int height = Console.WindowHeight;
        int width = Console.WindowWidth;
        int startX = 0;
        int startY = 0;

        string titleText = "CREATE/LOAD SAVE";
        string goBackText = "Back";


        Console.ForegroundColor = ConsoleColor.DarkRed;


        int titleYOffset = 3;
        int titleX = startX + (width - titleText.Length) / 2;
        int titleY = startY + height / 2 - titleYOffset;
        Console.SetCursorPosition(titleX, titleY);
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write(titleText);

        int savedGamesWindowHeight = 10;
        int savedGamesWindowWidth = 32;

        int savedGamesWindowX = (startX + (width - savedGamesWindowWidth) - 1) / 2;
        int savedGamesWindowY = startY + height / 2;

        int backToPreviousMenuX = (startX + (width - goBackText.Length) - 1) / 2;
        int backToPreviousMenuY = savedGamesWindowY + savedGamesWindowHeight + 2;
        Console.SetCursorPosition(backToPreviousMenuX, backToPreviousMenuY);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(goBackText);



        if (selection == LoadSavesScreenOption.Saves)
        {
            Console.SetCursorPosition(backToPreviousMenuX - 3, backToPreviousMenuY);
            Console.Write(' ');
            Console.SetCursorPosition(backToPreviousMenuX - 2, backToPreviousMenuY);
            Console.Write(' ');

            DrawABox(savedGamesWindowHeight, savedGamesWindowWidth, savedGamesWindowX, savedGamesWindowY, '-', '|', '+', '+', '+', '+', ConsoleColor.White);
            FillTextInsideBox(' ', savedGamesWindowHeight, savedGamesWindowWidth, savedGamesWindowX, savedGamesWindowY);

            WriteTextCenteredInBox(saveGameInfo, savedGamesWindowHeight, savedGamesWindowWidth, savedGamesWindowX, savedGamesWindowY);


        }
        else
        {
            Console.SetCursorPosition(backToPreviousMenuX - 3, backToPreviousMenuY);
            Console.Write('=');
            Console.SetCursorPosition(backToPreviousMenuX - 2, backToPreviousMenuY);
            Console.Write('>');
            DrawABox(savedGamesWindowHeight, savedGamesWindowWidth, savedGamesWindowX, savedGamesWindowY, ' ', ' ', ' ', ' ', ' ', ' ', ConsoleColor.Gray);
            FillTextInsideBox(' ', savedGamesWindowHeight, savedGamesWindowWidth, savedGamesWindowX, savedGamesWindowY);

            WriteTextCenteredInBox(saveGameInfo, savedGamesWindowHeight, savedGamesWindowWidth, savedGamesWindowX, savedGamesWindowY, ConsoleColor.Gray);
        }

        Console.ResetColor();



    }
    public void WriteTextCenteredInBox(string[] textLines, int boxHeight, int boxWidth, int boxXpos, int boxYpos, ConsoleColor consoleColor = ConsoleColor.White)
    {
        Console.ForegroundColor = consoleColor;
        // Calculate the interior dimensions (excluding border)
        int interiorHeight = boxHeight - 2;
        int interiorWidth = boxWidth - 2;

        // Calculate starting position to center the text block vertically
        int textBlockHeight = textLines.Length;
        int startRow = (interiorHeight - textBlockHeight) / 2;

        // Make sure we don't go outside the box
        if (startRow < 0) startRow = 0;

        for (int i = 0; i < textLines.Length && i < interiorHeight; i++)
        {
            string line = textLines[i];

            // Truncate line if it's too long for the box
            if (line.Length > interiorWidth)
            {
                line = line.Substring(0, interiorWidth);
            }

            // Calculate horizontal center position
            int startCol = (interiorWidth - line.Length) / 2;
            if (startCol < 0) startCol = 0;

            // Set cursor position (adding 1 to account for border)
            Console.SetCursorPosition(boxXpos + 1 + startCol, boxYpos + 1 + startRow + i);
            Console.Write(line);
        }
    }

    public void RenderUIStats(Character character, int turn, int height, int width, int startX, int startY)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        DrawABox(height, width, startX, startY, '-', '|', '+', '+', '+', '+');

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

    public void DrawABox(int height, int width, int startX, int startY, char horizontalLine, char verticalLine, char upperLeftCorner, char upperRightCorner, char lowerLeftCorner, char lowerRightCorner, ConsoleColor consoleColor = ConsoleColor.White)
    {
        Console.ForegroundColor = consoleColor;

        // TOP
        Console.SetCursorPosition(startX, startY);
        Console.Write(upperLeftCorner);
        for (int i = 1; i < width - 1; i++)
            Console.Write(horizontalLine);
        Console.Write(upperRightCorner);

        // BOTH SIDES
        for (int i = 1; i < height - 1; i++)
        {
            Console.SetCursorPosition(startX, startY + i);
            Console.Write(verticalLine);
            Console.SetCursorPosition(startX + width - 1, startY + i);
            Console.Write(verticalLine);
        }

        // BOTTOM
        Console.SetCursorPosition(startX, startY + height - 1);
        Console.Write(lowerLeftCorner);
        for (int i = 1; i < width - 1; i++)
            Console.Write(horizontalLine);
        Console.Write(lowerRightCorner);



    }
    public enum StartScreenOption
    {
        Start,
        Quit
    }
    public enum LoadSavesScreenOption
    {
        Back,
        Saves
    }
}
