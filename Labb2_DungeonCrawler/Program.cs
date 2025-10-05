using Labb2_DungeonCrawler.Core;



GameState gameState = new GameState();
Console.CursorVisible = false;

Gameloop gameLoop = new Gameloop(gameState);

gameLoop.PlayGame();
