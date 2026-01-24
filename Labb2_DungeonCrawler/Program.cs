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
                bool continueCheck = true;
                while (continueCheck)
                {
                    renderer.DisplayLoadSaveScreen(loadSelectedOption);
                    var selectSaveInput = Console.ReadKey();
                    switch (selectSaveInput.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (loadSelectedOption == Renderer.LoadSavesScreenOption.Back)
                                loadSelectedOption = Renderer.LoadSavesScreenOption.Saves;
                            break;
                        case ConsoleKey.DownArrow:
                            if (loadSelectedOption == Renderer.LoadSavesScreenOption.Saves)
                                loadSelectedOption = Renderer.LoadSavesScreenOption.Back;
                            break;
                        case ConsoleKey.Enter:
                            if (loadSelectedOption == Renderer.LoadSavesScreenOption.Saves)
                            {
                                Console.Clear();
                                Gameloop gameLoop = new Gameloop(enemyRepository, templateRepo, saveGameRepository);
                                await gameLoop.InitializeAsync();
                                await gameLoop.PlayGame();
                            }
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            continueCheck = false;
                            break;
                    }
                }
            }
            else
                Console.Clear();
            Environment.Exit(0);
            break;
    }
}


