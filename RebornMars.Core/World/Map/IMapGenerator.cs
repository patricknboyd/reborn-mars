using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Map
{
    /// <summary>
    /// Provides methods for generating dungeon maps.
    /// </summary>
    public interface IMapGenerator
    {
        /// <summary>
        /// Generate a new map.
        /// </summary>
        /// <param name="width">The width, in tiles, of the new map.</param>
        /// <param name="height">The height, in tiles, of the new map.</param>
        /// <returns></returns>
        IMap GenerateMap(int width, int height);
    }
}
