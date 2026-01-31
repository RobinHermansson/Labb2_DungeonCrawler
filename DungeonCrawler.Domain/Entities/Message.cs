namespace DungeonCrawler.Domain.Entities;

public class Message
{
    public string Text { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

    public Message()
    {
        
    }

    public Message(string message)
    {
        Text = $"{TimeStamp} - {message}";
    }

    public override string ToString()
    {
        return Text;
    }

}
