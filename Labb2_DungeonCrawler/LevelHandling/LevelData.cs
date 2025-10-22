using Labb2_DungeonCrawler.LevelElements;

namespace Labb2_DungeonCrawler.LevelHandling;

public class LevelData
{
    private List<LevelElement> _elements = new List<LevelElement>();

    public List<LevelElement> LevelElementsList
    {
        get
        {
            return _elements;
        }
    }

    public void LoadElementsFromFile(string filepath)
    {
        var numberOfCharacters = File.ReadLines(filepath).Sum(s => s.Length);
        int ypos = 0;
        int xpos = 0;
        using (StreamReader reader = new StreamReader(filepath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                foreach (char character in line)
                {
                    if (character == '#')
                    {
                        _elements.Add(new Wall(new Position(xpos, ypos), character, ConsoleColor.Black));
                    }
                    if (character == 'r')
                    {
                        _elements.Add(new Rat(new Position(xpos, ypos), character, ConsoleColor.Black));
                    }
                    if (character == 's')
                    {
                        _elements.Add(new Snake(new Position(xpos, ypos), character, ConsoleColor.Black));
                    }
                    if (character == '@')
                    {
                        _elements.Add(new Player("Player", new Position(xpos, ypos), character, ConsoleColor.Yellow));
                    }
                    xpos++;

                }
                xpos = 0;
                ypos++;
            }

        }

    }
}
