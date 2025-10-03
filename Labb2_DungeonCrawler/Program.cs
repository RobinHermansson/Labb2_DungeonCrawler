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
        // TODO: Create a gameloop..
    player.Draw();

    foreach (var element in gameState.LevelData.LevelElementsList)
    {
        if (element is Rat enemyRat)
        {
            oldPosition = enemyRat.Position;
            Console.SetCursorPosition(oldPosition.XPos, oldPosition.YPos);
            Console.Write(' ');
            enemyRat.Update();
            enemyRat.Draw();
        }
    }

}

