using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Map
{
    /// <summary>
    /// Represents a single tile on a map.
    /// </summary>
    public interface IMapTile
    {
        Position Position { get; set; }

        /// <summary>
        /// Gets a description of the tile type.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets whether the tile is able to be walked on.
        /// </summary>
        bool IsWalkable { get; }
        /// <summary>
        /// Gets whether the tile can be flown over.
        /// </summary>
        bool IsFlyable { get; }
        /// <summary>
        /// Gets whether the tile can be swam through.
        /// </summary>
        bool IsSwimmable { get; }

        IMapTile Left { get; }
        IMapTile UpLeft { get; }
        IMapTile Up { get; }
        IMapTile UpRight { get; }
        IMapTile Right { get; }
        IMapTile DownRight { get; }
        IMapTile Down { get; }
        IMapTile DownLeft { get; }
    }
}
