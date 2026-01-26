
using DungeonCrawler.App.Services;
using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Domain.ValueObjects;
using Labb2_DungeonCrawler.App.Utilities;
using static Labb2_DungeonCrawler.App.Core.Renderer;

namespace Labb2_DungeonCrawler.App.Core;

public class Gameloop
{
    private readonly ILevelTemplateRepository _levelTemplateRepository;
    private readonly ISaveGameRepository _saveGameRepository;
    private readonly IPlayerClassRepository _playerClassRepository;
    private GameService _gameService;
    public GameState GameState { get; set; }
    public Player Player { get; set; }
    public Renderer Renderer { get; private set; }
    public DebugAssistant Debugger { get; set; }

    public int UIXStartPos { get; set; } = 53;
    public int UIYStartPos { get; set; } = 0;
    public int UIHeight { get; set; } = 12;
    public int UIWidth { get; set; } = 18;
    public int InstructionsXPos { get; private set; } = 0;
    public int InstructionsYPos { get; private set; } = 20;
    public int MessageLogXPos { get; private set; } = 0;
    public int MessageLogYPos { get; private set; } = 22;
    public int DebugTextXPos { get; private set; } = 0;
    public int DebugTextYPos { get; private set; } = 21;
    public int DebugSelectorXPos { get; private set; } = 0;
    public int DebugSelectorYPos { get; set; } = 0;

    public Gameloop(GameService gameService, ILevelTemplateRepository levelTemplateRepository, ISaveGameRepository saveGameRepository, IPlayerClassRepository playerClassRepo)
    {
        _levelTemplateRepository = levelTemplateRepository;
        _saveGameRepository = saveGameRepository;
        _playerClassRepository = playerClassRepo;
        _gameService = gameService;
    }

    public async Task InitializeAsync(SaveGame selectedSave, int slotNumber, string? wantedPlayerName, PlayerClass playerClass)
    {
        await InitializeGame(selectedSave, slotNumber, wantedPlayerName, playerClass);
    }

    private async Task InitializeGame(SaveGame selectedSave, int slotNumber, string? wantedPlayerName, PlayerClass playerClass)
    {
        Console.CursorVisible = false;

        

        if (selectedSave == null) // Empty slot - create new game
        {
            GameState = await _gameService.CreateNewGameAsync(wantedPlayerName, playerClass);
            await _gameService.SaveGameAsync(GameState, slotNumber);
        }
        else // Existing save - load it
        {
            GameState = selectedSave.ToGameState();

            if (selectedSave.PlayerClassId.HasValue)
            {
                var existingPlayerClass = await _playerClassRepository.GetByIdAsync(selectedSave.PlayerClassId.Value);
                GameState.Player.ApplyPlayerClassStatsPreserveHP(existingPlayerClass);
            }
        }
        GameState.SlotNumber = slotNumber;

        // Set game state references for enemies
        foreach (var enemy in GameState.Enemies)
        {
            enemy.GameState = GameState; // TODO: Improve this later
        }
        Player = GameState.Player;
        Player.Name = GameState.PlayerName;
        if (GameState.MessageLog is null)
        {
            GameState.MessageLog = new MessageLog();
        }
        Renderer = new Renderer(GameState.MessageLog);
        Player.CheckSurrounding(GameState.AllElements);
        Renderer.RenderLevel(GameState.AllElements);
        Renderer.RenderUIStats(character: Player, turn: GameState.Turn, height: UIHeight, width: UIWidth, startX: UIXStartPos, startY: UIYStartPos);
        Renderer.DrawInstructions(InstructionsXPos, InstructionsYPos);
        Renderer.WriteMessageLog(MessageLogXPos, MessageLogYPos);
    }

    private bool ProcessPlayerAction()
    {
        ConsoleKeyInfo input = Console.ReadKey(true);
        if (input.Key == ConsoleKey.Escape)
        {
            return true; //  pause was requested
        }

        if (input.Key == ConsoleKey.L)
        {
            Renderer.DrawHistoryLogScreen();
            Renderer.RenderLevel(GameState.AllElements);
            Renderer.RenderUIStats(character: Player, turn: GameState.Turn, height: UIHeight, width: UIWidth, startX: UIXStartPos, startY: UIYStartPos);
            Renderer.DrawInstructions(InstructionsXPos, InstructionsYPos);
            Renderer.WriteMessageLog(MessageLogXPos, MessageLogYPos);
            return false; // Return early, don't process as movement
        }


        Position? selectedMove = Player.MovementHandler(input);
        if (selectedMove is Position attempt)
        {

            Enemy? enemyAtPosition = GameState.Enemies
                .FirstOrDefault(enemy => enemy.Position.XPos == attempt.XPos &&
                                        enemy.Position.YPos == attempt.YPos);
            if (enemyAtPosition != null)
            {
                // Found an enemy at this position - initiate combat!
                Combat combat = new Combat(aggressor: Player, defender: enemyAtPosition, GameState.MessageLog);
                combat.StartCombat();
                GameState.FightHappened = true;
            }
            else if (GameState.Player.AttemptMove(attempt, GameState))
            {
                Player.MoveTo(attempt);
            }
        }
        Player.CheckSurrounding(GameState.AllElements);
        return false; // did not Pause.
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

    public async Task PlayGame()
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
                        if (cki.Key == ConsoleKey.Escape)
                        {
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

            GameState.Turn++;
            bool pauseRequested = ProcessPlayerAction();


            if (pauseRequested)
            {
                var pauseResult = await ShowPauseMenuAsync();

                switch (pauseResult)
                {
                    case PauseScreenOption.Resume:
                        Renderer.RenderLevel(GameState.AllElements);
                        Renderer.RenderUIStats(character: Player, turn: GameState.Turn, height: UIHeight, width: UIWidth, startX: UIXStartPos, startY: UIYStartPos);
                        Renderer.DrawInstructions(InstructionsXPos, InstructionsYPos);
                        Renderer.WriteMessageLog(MessageLogXPos, MessageLogYPos);
                        continue; 

                    case PauseScreenOption.SaveAndQuit:
                        await _gameService.SaveGameAsync(GameState, GameState.SlotNumber);
                        isGameRunning = false;
                        break;

                    case PauseScreenOption.Quit:
                        isGameRunning = false;
                        break;
                }

                if (!isGameRunning)
                    break;             
            }
            else
            {
                Renderer.RenderUIStats(character: Player, turn: GameState.Turn, height: UIHeight, width: UIWidth, startX: UIXStartPos, startY: UIYStartPos);
                Renderer.DrawInstructions(InstructionsXPos, InstructionsYPos);
                Renderer.WriteMessageLog(MessageLogXPos, MessageLogYPos);
            }


            ProcessEnemyMovement();
            Renderer.RenderUIStats(character: Player, turn: GameState.Turn, height: UIHeight, width: UIWidth, startX: UIXStartPos, startY: UIYStartPos);
            Renderer.DrawInstructions(InstructionsXPos, InstructionsYPos);
            Renderer.WriteMessageLog(MessageLogXPos, MessageLogYPos);


            if (GameState.FightHappened)
            {
                Renderer.RenderLevel(GameState.AllElements);
                GameState.FightHappened = false; //Reset.
            }
            ProcessEnemyDeath();

            if (!Player.IsAlive())
            {
                Renderer.DisplayGameOver();
                isGameRunning = false;
            }
            if (GameState.Turn % 20 == 0)
            {
                await _gameService.SaveGameAsync(GameState, GameState.SlotNumber);

            }

        }
    }
    private async Task<PauseScreenOption> ShowPauseMenuAsync()
    {
        PauseScreenOption selectedOption = PauseScreenOption.Resume;
        Console.Clear();
        while (true)
        {
            Renderer.DrawPauseScreen(selectedOption);
            var input = Console.ReadKey(true);

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedOption == PauseScreenOption.Quit)
                        selectedOption = PauseScreenOption.SaveAndQuit;
                    else if (selectedOption == PauseScreenOption.SaveAndQuit)
                        selectedOption = PauseScreenOption.Resume;
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedOption == PauseScreenOption.Resume)
                        selectedOption = PauseScreenOption.SaveAndQuit;
                    else if (selectedOption == PauseScreenOption.SaveAndQuit)
                        selectedOption = PauseScreenOption.Quit;
                    break;

                case ConsoleKey.Enter:
                    Console.Clear();
                    return selectedOption;

                case ConsoleKey.Escape:
                    // Escape also resumes
                    return PauseScreenOption.Resume;
            }
        }
    }

}
