using Boyd.Games.RebornMars.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Map
{
    /// <summary>
    /// Represents a basic floor tile.
    /// </summary>
    public class FloorTile : MapTileBase
    {
        public FloorTile()
        {
            IsWalkable = true;
            IsFlyable = true;
            IsSwimmable = false;
            Description = TileDescriptions.FloorTile;
        }
    }
}
