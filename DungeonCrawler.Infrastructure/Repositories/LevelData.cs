using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.ValueObjects;
namespace DungeonCrawler.Infrastructure.Repositories;

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
   
}
