
using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.ValueObjects;
using DungeonCrawler.Infrastructure.Repositories;

namespace DungeonCrawler.Infrastructure.Disc;

public class DiscRepository
{
    public async Task<List<LevelElement>> LoadLevelElementsAsync(int levelNumber = 1)
    {

        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, "Levels", $"Level{levelNumber}.txt");
        return await LoadLevelElements(path);
    }

    private async Task<List<LevelElement>> LoadLevelElements(string filepath)
    {
        var numberOfCharacters = File.ReadLines(filepath).Sum(s => s.Length);
        int ypos = 0;
        int xpos = 0;
        List<LevelElement> elements = new();
        using (StreamReader reader = new StreamReader(filepath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                foreach (char character in line)
                {
                    if (character == '#')
                    {
                        elements.Add(new Wall(new Position(xpos, ypos), character, ConsoleColor.Black));
                    }
                    if (character == 'r')
                    {
                        elements.Add(new Rat(new Position(xpos, ypos), character, ConsoleColor.Black));
                    }
                    if (character == 's')
                    {
                        elements.Add(new Snake(new Position(xpos, ypos), character, ConsoleColor.Black));
                    }
                    if (character == '@')
                    {
                        elements.Add(new Player("Player", new Position(xpos, ypos), character, ConsoleColor.Yellow));
                    }
                    xpos++;

                }
                xpos = 0;
                ypos++;
            }

        }
        return elements;
    }
}


