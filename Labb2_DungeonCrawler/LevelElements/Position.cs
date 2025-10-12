namespace Labb2_DungeonCrawler.LevelElements;

public struct Position
{

    public int XPos { get; set; }
    public int YPos { get; set; }
    public Position(int xpos, int ypos)
    {
        XPos = xpos;
        YPos = ypos;
    }

    public static Position operator +(Position delta, Position currentPos)
    {
        return new Position(currentPos.XPos + delta.XPos, currentPos.YPos + delta.YPos);
    }

    public static bool operator ==(Position firstPos, Position secondPos)
    {
        if (firstPos.XPos == secondPos.XPos && firstPos.YPos == secondPos.YPos)
        {
            return true;
        }
        return false;
    }

    public static bool operator !=(Position firstPos, Position secondPos)
    {
        if (firstPos.XPos != secondPos.XPos || firstPos.YPos != secondPos.YPos)
        {
            return false;
        }
        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj is Position other)
        {
            return XPos == other.XPos && YPos == other.YPos;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return XPos.GetHashCode() ^ YPos.GetHashCode();
    }
}
