using Labb2_DungeonCrawler.LevelElements;
using Labb2_DungeonCrawler.LevelHandling;
using System.Linq.Expressions;

var levelData = new LevelData();
string path = @"C:\Users\robin\source\repos\Labb2_DungeonCrawler\Labb2_DungeonCrawler\Levels\Level1.txt";

levelData.LoadElementsFromFile(path);

Player player = null;
foreach (var element in levelData.LevelElementsList)
{
    if (element.RepresentationAsChar == '@')
    {
        player = (Player)element;   
    }
    element.Draw();
}

Console.CursorVisible = false;
while (true)
{
    
    ConsoleKeyInfo input = Console.ReadKey();
    Position oldPosition = player.Position;
    Console.SetCursorPosition(oldPosition.XPos, oldPosition.YPos);
    Console.Write(' ');

    if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow)
    {
        player.Position = new Position(oldPosition.XPos, oldPosition.YPos-1);
    }
    if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow)
    {
        player.Position = new Position(oldPosition.XPos, oldPosition.YPos+1);
    }
    if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow)
    {        
        player.Position = new Position(oldPosition.XPos-1, oldPosition.YPos);
    }
    if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow)
    {
        player.Position = new Position(oldPosition.XPos+1, oldPosition.YPos);
    }

    // TODO: Create a gameloop..
    player.Draw();
}

