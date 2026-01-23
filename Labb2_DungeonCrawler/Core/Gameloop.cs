
using DungeonCrawler.App.Services;
using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.ValueObjects;
using DungeonCrawler.Infrastructure.Disc;
using Labb2_DungeonCrawler.App.Utilities;
using DungeonCrawler.Domain.Interfaces;

namespace Labb2_DungeonCrawler.App.Core;

public class Gameloop
{
    private readonly IEnemyRepository _enemyRepository;
    private readonly ILevelTemplateRepository _levelTemplateRepository;
    public GameState GameState { get; set; }
    public Player Player { get; set; }
    public Renderer Renderer { get; private set; }
    public DebugAssistant Debugger { get; set; }

    public int Turn { get; set; }
    public int UIXStartPos { get; set; } = 53;
    public int UIYStartPos { get; set; } = 0;
    public int UIHeight { get; set; } = 12;
    public int UIWidth { get; set; } = 18;
    public int InstructionsXPos { get; private set; } = 0;
    public int InstructionsYPos { get; private set; } = 20;
    public int DebugTextXPos { get; private set; } = 0;
    public int DebugTextYPos { get; private set; } = 21;
    public int DebugSelectorXPos { get; private set; } = 0;
    public int DebugSelectorYPos { get; set; } = 0;

    public Gameloop(IEnemyRepository enemyRepository, ILevelTemplateRepository levelTemplateRepository)
    {
        _levelTemplateRepository = levelTemplateRepository;
        _enemyRepository = enemyRepository;

        Turn = 0;
        //GameState = gameState;
        //Player = gameState.Player;
        Renderer = new Renderer();

        

        //InitializeGame();
    }

    public async Task InitializeAsync()
    {
        await InitializeGame();
    }

    private async Task InitializeGame()
    {
        Console.CursorVisible = false;

        DiscRepository discRepository = new DiscRepository();
        GameService gameService = new GameService(discRepository, _enemyRepository, _levelTemplateRepository);
        GameState = await gameService.CreateNewGameAsync();

        // Set game state references for enemies
        foreach (var enemy in GameState.Enemies)
        {
            enemy.GameState = GameState; // TODO: Improve this later
        }
        Player = GameState.Player;
        Player.CheckSurrounding(GameState.AllElements);
        Renderer.RenderLevel(GameState.AllElements);
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
                GameState.FightHappened = true;
            }
            else if (GameState.Player.AttemptMove(attempt, GameState))
            {
                Player.MoveTo(attempt);
            }
        }
        Player.CheckSurrounding(GameState.AllElements);
    }

    private void ProcessDebuggerMovement(ConsoleKeyInfo input)
    {
        //ConsoleKeyInfo input = Console.ReadKey(true);

        Position? selectedMove = Debugger.MovementHandler(input);
        if (selectedMove is Position legalMove)
            Debugger.MoveTo(legalMove);

        Renderer.DebugDraw(Debugger);

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
            GameState.AllElements.Remove(deadEnemy);
            GameState.Enemies.Remove(deadEnemy);
            Renderer.UnsubscribeElement(deadEnemy);
        }
    }

    public void PlayGame()
    {

        bool isGameRunning = true;

        while (isGameRunning)
        {
            if (GameState.Debug)
            {
                ConsoleKeyInfo cki;
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.F1)
                {
                    Console.SetCursorPosition(DebugTextXPos, DebugTextYPos + 2);
                    Console.WriteLine("DEBUG ON!");
                    Dictionary<Position, LevelElement> freshEntityDict = new();
                    foreach (var entity in GameState.AllElements) 
                    {
                        freshEntityDict[entity.Position] = entity;
                    }
                        
                    Debugger = new DebugAssistant(new Position(0, 2), freshEntityDict);
                    while (true)
                    {
                        cki = Console.ReadKey(true);
                        if (cki.Key == ConsoleKey.Escape){
                            break;
                        }
                        ProcessDebuggerMovement(cki);
                        Console.SetCursorPosition(DebugTextXPos, DebugTextYPos + 3);
                        if (Debugger.IsObjectOnPosition()) 
                        {
                            var objectOnPos = Debugger.GetObjectDataWhereStanding();
                            Renderer.DrawDebugValues(GameState.Debug, DebugTextXPos, DebugTextYPos, objectOnPos);
                        }
                    }
                }
            }

            Turn++;
            ProcessPlayerMovement();
            Renderer.RenderUIStats(character: Player, turn: Turn, height: UIHeight, width: UIWidth, startX: UIXStartPos, startY: UIYStartPos);
            Renderer.DrawInstructions(InstructionsXPos, InstructionsYPos);



            ProcessEnemyMovement();
            Renderer.RenderUIStats(character: Player, turn: Turn, height: UIHeight, width: UIWidth, startX: UIXStartPos, startY: UIYStartPos);
            Renderer.DrawInstructions(InstructionsXPos, InstructionsYPos);
            
            if (GameState.FightHappened)
            {
                Renderer.RenderLevel(GameState.AllElements);
                GameState.FightHappened = false; //Reset.
            }
            ProcessEnemyDeath();

            if (!Player.IsAlive())
            {
                isGameRunning = false;
                Renderer.DisplayGameOver();
            }

        }
    }
}
