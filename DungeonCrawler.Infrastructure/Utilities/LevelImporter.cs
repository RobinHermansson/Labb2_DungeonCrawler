using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Domain.ValueObjects;

namespace DungeonCrawler.Infrastructure.Utilities;

public class LevelImporter
{
    private readonly ILevelTemplateRepository _levelTemplateRepository;
    
    public LevelImporter(ILevelTemplateRepository levelTemplateRepository)
    {
        _levelTemplateRepository = levelTemplateRepository;
    }
    
    public async Task ImportFromFileAsync(string filepath, int levelNumber, string name = "")
    {
        var elements = new List<LevelElement>();
        int ypos = 0;
        int xpos = 0;
        
        using (StreamReader reader = new StreamReader(filepath))
        {
            while (!reader.EndOfStream)
            {
                string line = await reader.ReadLineAsync() ?? "";
                foreach (char character in line)
                {
                    LevelElement? element = character switch
                    {
                        '#' => new Wall(new Position(xpos, ypos), character, ConsoleColor.Black),
                        'r' => new Rat(new Position(xpos, ypos), character, ConsoleColor.Black),
                        's' => new Snake(new Position(xpos, ypos), character, ConsoleColor.Black),
                        '@' => new Player("Player", new Position(xpos, ypos), character, ConsoleColor.Yellow),
                        _ => null
                    };
                    
                    if (element != null)
                    {
                        elements.Add(element);
                    }
                    xpos++;
                }
                xpos = 0;
                ypos++;
            }
        }
        
        var levelTemplate = new LevelTemplate
        {
            LevelNumber = levelNumber,
            Name = name != "" ? name : $"Level {levelNumber}",
            InitialElements = elements
        };
        
        await _levelTemplateRepository.SaveAsync(levelTemplate);
    }
}