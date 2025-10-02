using Labb2_DungeonCrawler.LevelElements;
using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Utilities;



GameState gameState = new GameState();
Player player = gameState.Player;
Console.CursorVisible = false;
foreach (var element in gameState.LevelData.LevelElementsList)
{
    element.Draw();
}

Console.CursorVisible = false;
while (true)
{
    
    ConsoleKeyInfo input = Console.ReadKey();
    Position oldPosition = player.Position;
    Console.SetCursorPosition(oldPosition.XPos, oldPosition.YPos);
    Console.Write(' ');

    switch (input.Key)
    {
        case ConsoleKey.W:
            player.Position = DirectionTransformer.GetPositionDelta(Direction.Up) + player.Position;
            break;
        case ConsoleKey.S:
            player.Position = DirectionTransformer.GetPositionDelta(Direction.Down) + player.Position;
            break;
        case ConsoleKey.A:
            player.Position = DirectionTransformer.GetPositionDelta(Direction.Left) + player.Position;
            break;
        case ConsoleKey.D:
            player.Position = DirectionTransformer.GetPositionDelta(Direction.Right) + player.Position;
            break;
        case ConsoleKey.UpArrow:
            player.Position = DirectionTransformer.GetPositionDelta(Direction.Up) + player.Position;
            break;
        case ConsoleKey.DownArrow:
            player.Position = DirectionTransformer.GetPositionDelta(Direction.Down) + player.Position;
            break;
        case ConsoleKey.LeftArrow:
            player.Position = DirectionTransformer.GetPositionDelta(Direction.Left) + player.Position;
            break;
        case ConsoleKey.RightArrow:
            player.Position = DirectionTransformer.GetPositionDelta(Direction.Right) + player.Position;
            break;
    }
    /*
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
    */
    // TODO: Create a gameloop..
    player.Draw();
}

