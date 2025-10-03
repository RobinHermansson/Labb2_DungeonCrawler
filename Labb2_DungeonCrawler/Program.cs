using Labb2_DungeonCrawler.LevelElements;
using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Utilities;



GameState gameState = new GameState();
Player player = gameState.Player;
Console.CursorVisible = false;

Gameloop gameLoop = new Gameloop(gameState);

gameLoop.PlayGame();
