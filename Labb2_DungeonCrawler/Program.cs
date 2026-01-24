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

Renderer renderer = new Renderer();
Renderer.StartScreenOption selectedOption = Renderer.StartScreenOption.Start;
Renderer.LoadSavesScreenOption loadSelectedOption = Renderer.LoadSavesScreenOption.Saves;

var client = new MongoClient("mongodb://localhost:27017/");
var mongoDatabase = client.GetDatabase("DungeonCrawler");

var enemyRepository = new MongoEnemyRepository(mongoDatabase);

ILevelTemplateRepository templateRepo = new MongoLevelTemplateRepository(mongoDatabase);
ISaveGameRepository saveGameRepository = new SaveGameRepository(mongoDatabase);

IEnumerable<SaveGame> savedGames = await saveGameRepository.GetAllAsync();
List<SaveGame> savedGamesList = savedGames.ToList() ?? new List<SaveGame>();

var levelImporter = new LevelImporter(templateRepo);
string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
string path = Path.Combine(baseDirectory, "Levels", $"Level1.txt");
//await levelImporter.ImportFromFileAsync(path, 1);

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

                var saveGamesList = savedGames?.ToList() ?? new List<SaveGame>();

                SaveGame selectedSave = await SelectSaveGame(renderer, saveGamesList);

                if (selectedSave != null)
                {
                    Console.Clear();
                    Gameloop gameLoop = new Gameloop(enemyRepository, templateRepo, saveGameRepository);

                    await gameLoop.InitializeAsync(selectedSave);
                    await gameLoop.PlayGame();

                }
            }
            else
            {
                Console.Clear();
                Environment.Exit(0);
            }
            break;
    }

}
static async Task<SaveGame> SelectSaveGame(Renderer renderer, IList<SaveGame> savedGames)
{
    if (savedGames == null || savedGames.Count == 0)
        return null;

    int selectedSaveIndex = 0;
    Renderer.LoadSavesScreenOption loadSelectedOption = Renderer.LoadSavesScreenOption.Saves;

    while (true)
    {
        renderer.DisplayLoadSaveScreen(loadSelectedOption, savedGames, selectedSaveIndex);
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
                if (loadSelectedOption == Renderer.LoadSavesScreenOption.Saves && savedGames.Count > 1)
                {
                    selectedSaveIndex = (selectedSaveIndex - 1 + savedGames.Count) % savedGames.Count;
                }
                break;

            case ConsoleKey.RightArrow:
                if (loadSelectedOption == Renderer.LoadSavesScreenOption.Saves && savedGames.Count > 1)
                {
                    selectedSaveIndex = (selectedSaveIndex + 1) % savedGames.Count;
                }
                break;

            case ConsoleKey.Enter:
                if (loadSelectedOption == Renderer.LoadSavesScreenOption.Saves)
                {
                    return savedGames[selectedSaveIndex];                 }

                else
                {
                    return null; // User chose "Back"
                }

            case ConsoleKey.Escape:
                return null; // User cancelled
        }
    }
}




