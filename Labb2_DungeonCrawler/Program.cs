using DungeonCrawler.App.Services;
using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Infrastructure.Repositories;
using DungeonCrawler.Infrastructure.Repositories.Mongo;
using DungeonCrawler.Infrastructure.Repositories.Mongo.Mapping;
using DungeonCrawler.Infrastructure.Utilities;
using Labb2_DungeonCrawler.App.Core;
using MongoDB.Driver;

//GameState gameState = new GameState();
Console.CursorVisible = false;


MongoMappings.Register();


MessageLog messageLog = new(); // need to rethink this.
Renderer renderer = new Renderer(messageLog); // need to rethink this.
Renderer.StartScreenOption selectedOption = Renderer.StartScreenOption.Start;
Renderer.LoadSavesScreenOption loadSelectedOption = Renderer.LoadSavesScreenOption.Saves;

var client = new MongoClient("mongodb://localhost:27017/");
var mongoDatabase = client.GetDatabase("DungeonCrawler");

IPlayerClassRepository pcRepo = new MongoPlayerClassRepository(mongoDatabase);
PlayerClassSeeder pcSeeder = new PlayerClassSeeder(pcRepo);
await pcSeeder.SeedDefaultClassesAsync();

var availableClasses = await pcRepo.GetAllAsync();
var availableClassesList = availableClasses.ToList();

ILevelTemplateRepository templateRepo = new MongoLevelTemplateRepository(mongoDatabase);
ISaveGameRepository saveGameRepository = new SaveGameRepository(mongoDatabase);
SaveGameService saveGameService = new SaveGameService(saveGameRepository, pcRepo);

SaveSlot[] saveSlots = await BuildSaveSlots(saveGameRepository, pcRepo);
GameService gameService = new GameService(templateRepo, saveGameRepository);

var setupService = new InitialSetupService(pcRepo, templateRepo);
await setupService.InitializeDatabaseAsync();

//var levelImporter = new LevelImporter(templateRepo);
//tring baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
//string path = Path.Combine(baseDirectory, "Levels", $"Level1.txt");

Console.ForegroundColor = ConsoleColor.DarkRed;
renderer.DrawABox(Console.WindowHeight, Console.WindowWidth, 0, 0, '═', '║', '╔', '╗', '╚', '╝');
while (true)
{
    renderer.DisplayTitleScreen(selectedOption);
    var input = Console.ReadKey();
    switch (input.Key)
    {
        case ConsoleKey.UpArrow:
            if (selectedOption == Renderer.StartScreenOption.Quit)
                selectedOption = Renderer.StartScreenOption.Start;
            break;

        case ConsoleKey.DownArrow:
            if (selectedOption == Renderer.StartScreenOption.Start)
                selectedOption = Renderer.StartScreenOption.Quit;
            break;

        case ConsoleKey.Enter:
            if (selectedOption == Renderer.StartScreenOption.Start)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                renderer.DrawABox(Console.WindowHeight, Console.WindowWidth, 0, 0, '═', '║', '╔', '╗', '╚', '╝');

                var (selectedSave, slotNumber) = await SelectSaveGame(renderer, saveSlots);

                if (slotNumber > 0) // User selected a valid slot
                {
                    string? nameInput = "";
                    string? classInput = "";
                    int playerClassSelected = 0;
                    if (selectedSave == null)
                    {
                        bool tryAgain = false;
                        while (true)
                        {
                            renderer.SelectNameScreen(tryAgain);
                            nameInput = Console.ReadLine();

                            if (nameInput.Length <= 0)
                            {
                                tryAgain = true;
                            }
                            else
                            {
                                tryAgain = false;
                                break;
                            }
                        }
                        while (true)
                        {
                            renderer.SelectClassScreen(availableClassesList, tryAgain);
                            classInput = Console.ReadLine();
                            _ = int.TryParse(classInput, out playerClassSelected);
                            if (playerClassSelected <= 0 || playerClassSelected > availableClasses?.Count())
                            {
                                tryAgain = true;
                            }
                            else
                            {
                                playerClassSelected--; // Selection is between 1 to .Count, and the index starts at 0...
                                tryAgain = false;
                                break;
                            }
                        }
                        
                    }
                    Console.Clear();
                    Gameloop gameLoop = new Gameloop(saveGameService, gameService, templateRepo, pcRepo);

                    await gameLoop.InitializeAsync(selectedSave, slotNumber, nameInput, availableClassesList[playerClassSelected]);
                    await gameLoop.PlayGame();

                    if (!gameLoop.GameState.Player.IsAlive())
                    {
                        var saveToDelete = await saveGameRepository.GetBySlotNumberAsync(gameLoop.GameState.SlotNumber);
                        await saveGameRepository.RemoveAsync(saveToDelete);
                    }
                    // If the user saves and returns to this title screen, rebuild save slots after game ends to show updated information
                    saveSlots = await BuildSaveSlots(saveGameRepository, pcRepo);
                }
                // If slotNumber is 0, user went back, so continue the outer loop
                renderer.FillTextInsideBox(' ', Console.WindowHeight, Console.WindowWidth, 0, 0);
            }
            else
            {
                Console.Clear();
                Environment.Exit(0);
            }
            break;
    }

}
static async Task<(SaveGame selectedSave, int slotNumber)> SelectSaveGame(Renderer renderer, SaveSlot[] saveSlots)
{
    int selectedSlotIndex = 0;
    Renderer.LoadSavesScreenOption loadSelectedOption = Renderer.LoadSavesScreenOption.Saves;

    while (true)
    {
        renderer.DisplayLoadSaveScreen(loadSelectedOption, saveSlots, selectedSlotIndex);
        var input = Console.ReadKey();

        switch (input.Key)
        {
            case ConsoleKey.UpArrow:
                if (loadSelectedOption == Renderer.LoadSavesScreenOption.Back)
                    loadSelectedOption = Renderer.LoadSavesScreenOption.Saves;
                break;

            case ConsoleKey.DownArrow:
                if (loadSelectedOption == Renderer.LoadSavesScreenOption.Saves)
                    loadSelectedOption = Renderer.LoadSavesScreenOption.Back;
                break;

            case ConsoleKey.LeftArrow:
                if (loadSelectedOption == Renderer.LoadSavesScreenOption.Saves)
                {
                    selectedSlotIndex = (selectedSlotIndex - 1 + saveSlots.Length) % saveSlots.Length;
                }
                break;

            case ConsoleKey.RightArrow:
                if (loadSelectedOption == Renderer.LoadSavesScreenOption.Saves)
                {
                    selectedSlotIndex = (selectedSlotIndex + 1) % saveSlots.Length;
                }
                break;

            case ConsoleKey.Enter:
                if (loadSelectedOption == Renderer.LoadSavesScreenOption.Saves)
                {
                    var selectedSlot = saveSlots[selectedSlotIndex];
                    return (selectedSlot.SaveGame, selectedSlot.SlotNumber); // Return both save and slot number
                }
                else
                {
                    return (null, 0); // User chose "Back"
                }

            case ConsoleKey.Escape:
                return (null, 0); // User cancelled
        }
    }
}

static async Task<SaveSlot[]> BuildSaveSlots(ISaveGameRepository saveGameRepository, IPlayerClassRepository playerClassRepository)
{
    var allClasses = await playerClassRepository.GetAllAsync();
    var classLookup = allClasses.ToDictionary(pc => pc.Id, pc => pc.Name);
    var saveSlots = new SaveSlot[3];

    for (int i = 0; i < 3; i++)
    {
        saveSlots[i] = new SaveSlot { SlotNumber = i + 1, SaveGame = null };
    }

    var existingSaves = await saveGameRepository.GetAllAsync();
    foreach (var save in existingSaves)
    {
        if (save.SlotNumber >= 1 && save.SlotNumber <= 3)
        {
            saveSlots[save.SlotNumber - 1].SaveGame = save;
            if (save.PlayerClassId.HasValue && classLookup.TryGetValue(save.PlayerClassId.Value, out var className))
            {
                saveSlots[save.SlotNumber - 1].PlayerClassName = className;
            }
        }
    }

    return saveSlots;
}




