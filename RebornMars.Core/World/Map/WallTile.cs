using Boyd.Games.RebornMars.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Map
{
    /// <summary>
    /// Represents a basic wall tile.
    /// </summary>
    public class WallTile : MapTileBase
    {
        public WallTile()
        {
            IsWalkable = false;
            IsFlyable = false;
            IsSwimmable = false;
            Description = TileDescriptions.WallTile;
        }
    }
}
