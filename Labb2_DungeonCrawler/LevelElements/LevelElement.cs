using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_DungeonCrawler.LevelElements
{
    public abstract class LevelElement
    {

        public Position Position { get; set; }
        public char RepresentationAsChar { get; set; }
    }
}
