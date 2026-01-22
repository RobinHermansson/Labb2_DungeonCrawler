using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Infrastructure.Repositories;
using MongoDB.Driver;
using Labb2_DungeonCrawler.App.Core;

//GameState gameState = new GameState();
Console.CursorVisible = false;

Renderer renderer = new Renderer();
Renderer.StartScreenOption selectedOption = Renderer.StartScreenOption.Start;
var client = new MongoClient("mongodb://localhost:27017/");
var mongoDatabase = client.GetDatabase("characters");

ICharacterRepository = new MongoCharacterRepository
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
                Gameloop gameLoop = new Gameloop();
                gameLoop.PlayGame();
            }
            else
                Environment.Exit(0);
            break;
    }
}


