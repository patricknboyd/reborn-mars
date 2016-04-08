using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Map
{
    /// <summary>
    /// Exposes basic properties of a dungeon map.
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Gets the width of the map.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Gets the height of the map.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the collection of stairs on this map level.
        /// </summary>
        IEnumerable<Staircase> Stairs { get; }

        /// <summary>
        /// Gets or sets a tile.
        /// </summary>
        /// <param name="x">The x coordinate of the tile.</param>
        /// <param name="y">The y coordinate of the tile.</param>
        /// <returns>The tile object at that position.</returns>
        /// <remarks>
        /// (0,0) is always the bottom left.
        /// </remarks>
        IMapTile this[int x, int y] { get; set; }
        /// <summary>
        /// Gets or sets a tile.
        /// </summary>
        /// <param name="p">The position of the tile.</param>
        /// <returns>The tile object at that position.</returns>
        /// <remarks>
        /// (0,0) is always the bottom left.
        /// </remarks>
        IMapTile this[Position p] { get; set; }

        /// <summary>
        /// Gets the collection of tiles that make up the map.
        /// </summary>
        IEnumerable<IMapTile> MapTiles { get; }

        /// <summary>
        /// Gets the collection of stairs leading up.
        /// </summary>
        IEnumerable<Staircase> UpStairs { get; }
        /// <summary>
        /// Gets the collection of stairs leading down.
        /// </summary>
        IEnumerable<Staircase> DownStairs { get; }

        /// <summary>
        /// Adds a new staircase to the level.
        /// </summary>
        /// <param name="stairs">The new staircase to add. This function assumes the stairs are in a valid location.</param>
        /// <remarks>
        /// Used during map building, shouldn't be called otherwise.
        /// </remarks>
        void AddStaircase(Staircase stairs);

        /// <summary>
        /// Removes all the staircases currently placed on the level.
        /// </summary>
        /// <remarks>
        /// Used during map building, shouldn't be called otherwise.
        /// </remarks>
        void RemoveAllStairs();

        /// <summary>
        /// Assigns neighbours for each tile.
        /// </summary>
        void SetUpTileNeigbours();
    }
}
