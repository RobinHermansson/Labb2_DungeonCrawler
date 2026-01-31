using System.Threading.Tasks.Dataflow;

namespace DungeonCrawler.Domain.Entities;

public class MessageLog
{
    public List<Message> Messages { get; set; } = new();

    public MessageLog()
    {
        
    }
}
