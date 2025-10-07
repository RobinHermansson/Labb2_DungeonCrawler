using Labb2_DungeonCrawler.LevelElements;
namespace Labb2_DungeonCrawler.Core;

public class Gameloop
{
    public GameState GameState { get; set; }
    public Player Player { get; set; }
    public Renderer Renderer { get; private set; }
    public Gameloop(GameState gameState)
    {

        GameState = gameState;
        Player = gameState.Player;
        Player.CheckSurrounding(GameState.LevelData.LevelElementsList);
        Renderer = new Renderer();

        InitializeGame();


    }

    private void InitializeGame()
    {
        Console.CursorVisible = false;

        // Set game state references for enemies
        foreach (var enemy in GameState.LevelData.LevelElementsList.OfType<Enemy>())
        {
            enemy.GameState = GameState; // TODO: Improve this later
        }

        Player.CheckSurrounding(GameState.LevelData.LevelElementsList);
        Renderer.RenderLevel(GameState.LevelData.LevelElementsList);
    }


    public void PlayGame()
    {

        bool isGameRunning = true;

        while (isGameRunning)
        {

            ConsoleKeyInfo input = Console.ReadKey();
            Renderer.ClearPosition(Player.Position);

            Position attempt = Player.MovementHandler(input);
            Enemy? enemyAtPosition = GameState.LevelData.LevelElementsList
                .OfType<Enemy>() // Filter to only Enemy types
                .FirstOrDefault(enemy => enemy.Position.XPos == attempt.XPos &&
                                        enemy.Position.YPos == attempt.YPos);

            if (enemyAtPosition != null)
            {
                // Found an enemy at this position - initiate combat!
                Combat combat = new Combat(aggressor: Player, defender: enemyAtPosition);
                combat.StartCombat();
            }
            else if (Player.AttemptMove(attempt, GameState))
            {
                Player.MoveTo(attempt);
                Player.CheckSurrounding(GameState.LevelData.LevelElementsList);
            }
            Player.Draw();


            Enemy enemyWhoDied = null;
            foreach (var element in GameState.LevelData.LevelElementsList)
            {
                if (element is Enemy enemyObject)
                {
                    enemyObject.Update();
                    // After a combat, attempt to remove the enemy. Need to redraw everything as the Combat clears the screen and also the enemy is gone. FIX THIS IN A MORE PROPER WAY!
                    if (!enemyObject.IsAlive())
                    {
                        enemyWhoDied = enemyObject;

                    }
                }
            }
            GameState.LevelData.LevelElementsList.Remove(enemyWhoDied);

            Renderer.RenderLevel(GameState.LevelData.LevelElementsList);
            if (!Player.IsAlive())
            {
                isGameRunning = false;
                Renderer.DisplayGameOver();
            }
        }
    }
}
