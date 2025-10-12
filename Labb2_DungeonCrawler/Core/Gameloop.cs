using Labb2_DungeonCrawler.LevelElements;
namespace Labb2_DungeonCrawler.Core;

public class Gameloop
{
    public GameState GameState { get; set; }
    public Player Player { get; set; }
    public Renderer Renderer { get; private set; }

    public int Turn { get; set; }
    public int UIXStartPos { get; set; } = 53;
    public int UIYStartPos { get; set; } = 0;
    public int UIHeight { get; set; } = 12;
    public int UIWidth { get; set; } = 18;
    public int InstructionsXPos { get; private set; } = 0;
    public int InstructionsYPos { get; private set; } = 20;
    public int DebugXPos { get; private set; } = 0;
    public int DebugYPos { get; private set; } = 21;

    public Gameloop(GameState gameState)
    {

        Turn = 0;
        GameState = gameState;
        Player = gameState.Player;
        Player.CheckSurrounding(GameState.LevelData.LevelElementsList);
        Renderer = new Renderer();

        InitializeGame();
    }

    private void InitializeGame()
    {
        Console.CursorVisible = false;

        Player.CheckSurrounding(GameState.LevelData.LevelElementsList);
        Renderer.RenderLevel(GameState.ElementsDict);
        Renderer.RenderUIStats(character: Player, turn: Turn, height: UIHeight, width: UIWidth, startX: UIXStartPos, startY: UIYStartPos);
        Renderer.DrawInstructions(InstructionsXPos, InstructionsYPos);
    }

    private void ProcessPlayerMovement()
    {
        ConsoleKeyInfo input = Console.ReadKey(true);

        Position? selectedMove = Player.MovementHandler(input);
        if (selectedMove is Position attempt)
        {

            Enemy? enemyAtPosition = GameState.Enemies
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
            }
        }
        Player.CheckSurrounding(GameState.LevelData.LevelElementsList);
    }

    private void ProcessEnemyMovement()
    {
        foreach (var enemy in GameState.Enemies)
        {
            enemy.Update();
        }
    }

    private void ProcessEnemyDeath() 
    {
        List<Enemy> enemyDeaths = new List<Enemy>();
        foreach (var enemy in GameState.Enemies)
        {
            if (!enemy.IsAlive())
            {
                enemyDeaths.Add(enemy);

            }
        }    
        foreach (var deadEnemy in enemyDeaths)
        {
            GameState.LevelData.LevelElementsList.Remove(deadEnemy);
            GameState.Enemies.Remove(deadEnemy);
            GameState.ElementsDict.Remove(deadEnemy.Position);
        }
    }



    public void PlayGame()
    {

        bool isGameRunning = true;

        while (isGameRunning)
        {

            Turn++;
            Renderer.DrawDebugValues(GameState.Debug, DebugXPos, DebugYPos, Player);
            ProcessPlayerMovement();
            Renderer.RenderUIStats(character: Player, turn: Turn, height: UIHeight, width: UIWidth, startX: UIXStartPos, startY: UIYStartPos);
            Renderer.DrawInstructions(InstructionsXPos, InstructionsYPos);


            ProcessEnemyMovement();
            Renderer.RenderUIStats(character: Player, turn: Turn, height: UIHeight, width: UIWidth, startX: UIXStartPos, startY: UIYStartPos);
            Renderer.DrawInstructions(InstructionsXPos, InstructionsYPos);


            ProcessEnemyDeath();

            Renderer.RenderLevel(GameState.ElementsDict);
            if (!Player.IsAlive())
            {
                isGameRunning = false;
                Renderer.DisplayGameOver();
            }

        }
    }
}
