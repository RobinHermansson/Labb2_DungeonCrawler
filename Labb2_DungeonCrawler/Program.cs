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

    Position attempt = player.MovementHandler(input);
    if (player.AttemptMove(attempt, gameState))
    {
        player.MoveTo(attempt);
    }
    /*
    switch (attempt)
    {
        case ConsoleKey.W:
            if (player.AttemptMove(DirectionTransformer.GetPositionDelta(Direction.Up) + player.Position, gameState))
            {
                player.MoveTo((DirectionTransformer.GetPositionDelta(Direction.Up) + player.Position));
            }
            break;
        case ConsoleKey.S:
            if (player.AttemptMove(DirectionTransformer.GetPositionDelta(Direction.Down) + player.Position, gameState))
            {
                player.MoveTo((DirectionTransformer.GetPositionDelta(Direction.Down) + player.Position));
            }
            break;
        case ConsoleKey.A:
            if (player.AttemptMove(DirectionTransformer.GetPositionDelta(Direction.Left) + player.Position, gameState))
            {
                player.MoveTo((DirectionTransformer.GetPositionDelta(Direction.Left) + player.Position));
            }
            break;
        case ConsoleKey.D:
            if (player.AttemptMove(DirectionTransformer.GetPositionDelta(Direction.Right) + player.Position, gameState))
            {
                player.MoveTo((DirectionTransformer.GetPositionDelta(Direction.Right) + player.Position));
            }
            break;
        case ConsoleKey.UpArrow:
            if (player.AttemptMove(DirectionTransformer.GetPositionDelta(Direction.Up) + player.Position, gameState))
            {
                player.MoveTo(DirectionTransformer.GetPositionDelta(Direction.Up) + player.Position);
            }
            break;
        case ConsoleKey.DownArrow:
            if (player.AttemptMove(DirectionTransformer.GetPositionDelta(Direction.Down) + player.Position, gameState))            
            {
                player.MoveTo(DirectionTransformer.GetPositionDelta(Direction.Down) + player.Position);
            }
            break;
        case ConsoleKey.LeftArrow:
            if (player.AttemptMove(DirectionTransformer.GetPositionDelta(Direction.Left) + player.Position, gameState))
            {
                player.MoveTo((DirectionTransformer.GetPositionDelta(Direction.Left) + player.Position));
            }
            break;
        case ConsoleKey.RightArrow:
            if (player.AttemptMove(DirectionTransformer.GetPositionDelta(Direction.Right) + player.Position, gameState))
            {
                player.MoveTo((DirectionTransformer.GetPositionDelta(Direction.Right) + player.Position));
            }
            break;
    }*/
    // TODO: Create a gameloop..
    player.Draw();
}

