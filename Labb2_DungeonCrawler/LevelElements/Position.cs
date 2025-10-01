using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_DungeonCrawler.LevelElements
{
    public struct Position
    {

        public int XPos { get; set; }
        public int YPos { get; set; }
        public Position(int xpos, int ypos)
        {
            XPos = xpos;
            YPos = ypos;
        }
    }
}
