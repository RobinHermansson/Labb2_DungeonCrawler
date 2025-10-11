using Labb2_DungeonCrawler.LevelElements;
using Labb2_DungeonCrawler.CharacterClasses;
namespace Labb2_DungeonCrawler.Core;

public class Renderer
{
    public void RenderLevel(List<LevelElement> elements)
    {
        foreach (var element in elements)
        {
            if (element.isVisible && element is Character characterElement)
            {
                ClearPosition(characterElement.PreviousPosition);
                Draw(characterElement, characterElement.Color);
            }
            else if (!element.isVisible && element is Character nonVisibleCharacter)
            {
                ClearPosition(nonVisibleCharacter.Position);
                ClearPosition(nonVisibleCharacter.PreviousPosition);
            }
            else if (element is Wall && element.isVisible)
            {
                Draw(element, ConsoleColor.DarkYellow);
            }
            else if (element is Wall && element.hasBeenSeen && !element.isVisible)
            {
                Draw(element, ConsoleColor.DarkGray);
            }
        }
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
    public Player DisplaySelectClassScreen()
    {
        int selectedIndex = 0;
        string[] classes = { "Warrior", "Mage"};
        
        while (true)
        {
            Console.Clear();
            
            Console.SetCursorPosition(10, 5);
            Console.WriteLine("Select Your Class:");
            
            for (int i = 0; i < classes.Length; i++)
            {
                Console.SetCursorPosition(15, 7 + i);
                if (i == selectedIndex)
                {
                    Console.Write("=> " + classes[i]);
                }
                else
                {
                    Console.Write("   " + classes[i]);
                }
            }
            
            // Display class description
            ICharacterClass selectedClass = null;
            switch (selectedIndex)
            {
                case 0: selectedClass = new WarriorClass(); break;
                case 1: selectedClass = new MageClass(); break;
                //case 2: selectedClass = new RogueClass(); break;
            }
            
            Console.SetCursorPosition(10, 12);
            Console.WriteLine(selectedClass.GetClassDescription());
            
            // Handle input
            var key = Console.ReadKey(true);
            
            if (key.Key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (key.Key == ConsoleKey.DownArrow && selectedIndex < classes.Length - 1)
            {
                selectedIndex++;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                return CreatePlayerFromClass(selectedIndex);
            }
        }
    }

    private Player CreatePlayerFromClass(int classIndex)
    {
        ICharacterClass characterClass;
        
        switch (classIndex)
        {
            case 0: characterClass = new WarriorClass(); break;
            case 1: characterClass = new MageClass(); break;
            //case 2: characterClass = new RogueClass(); break;
            default: characterClass = new WarriorClass(); break;
        }
        
        return new Player("Player", new Position(10, 10), '@', ConsoleColor.White, characterClass);
    }
    public enum StartScreenOption
    {
        Start,
        Quit
    }
}
