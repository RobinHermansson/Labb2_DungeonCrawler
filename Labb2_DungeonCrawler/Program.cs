using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Infrastructure.Repositories.Mongo;
using MongoDB.Driver;
using Labb2_DungeonCrawler.App.Core;
using DungeonCrawler.Domain.ValueObjects;
using DungeonCrawler.Infrastructure.Repositories.Mongo.Mapping;
using DungeonCrawler.Infrastructure.Repositories;
using DungeonCrawler.Infrastructure.Utilities;

//GameState gameState = new GameState();
Console.CursorVisible = false;


MongoMappings.Register();

Renderer renderer = new Renderer();
Renderer.StartScreenOption selectedOption = Renderer.StartScreenOption.Start;
var client = new MongoClient("mongodb://localhost:27017/");
var mongoDatabase = client.GetDatabase("DungeonCrawler");

var enemyRepository = new MongoEnemyRepository(mongoDatabase);

ILevelTemplateRepository templateRepo = new MongoLevelTemplateRepository(mongoDatabase);
var levelImporter = new LevelImporter(templateRepo);
string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
string path = Path.Combine(baseDirectory, "Levels", $"Level1.txt");
await levelImporter.ImportFromFileAsync(path, 1);

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
                Gameloop gameLoop = new Gameloop(enemyRepository);
                await gameLoop.InitializeAsync();
                gameLoop.PlayGame();
            }
            else
                Environment.Exit(0);
            break;
    }
}


