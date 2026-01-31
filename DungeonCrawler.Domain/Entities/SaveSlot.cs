namespace DungeonCrawler.Domain.Entities;
public class SaveSlot
{
    public int SlotNumber { get; set; }
    public SaveGame? SaveGame { get; set; } // Null if empty
    public string? PlayerClassName {get; set;}
    public bool IsEmpty => SaveGame == null;
    
    public string GetDisplayName()
    {
        if (IsEmpty)
            return $"Empty Slot {SlotNumber}";
        else
            return $"Slot {SlotNumber}: {SaveGame?.PlayerName}";
    }
    
    public string[] GetDisplayInfo()
    {
        if (IsEmpty)
        {
            return new string[]
            {
                $"- EMPTY SLOT {SlotNumber} -",
                "",
                "Press ENTER to start",
                "a new game"
            };
        }
        else
        {
            return new string[]
            {
                $"Player: {SaveGame?.PlayerName}",
                $"Turn: {SaveGame?.Turn}",
                $"Last save: {SaveGame?.LastPlayedAt:yyyy-MM-dd HH:mm}",
                $"Class: {PlayerClassName ?? "Unknown"}"
            };
        }
    }
}