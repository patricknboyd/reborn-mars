using Boyd.Games.RebornMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.World
{
    public class DefaultMap : MapBase
    {
        public DefaultMap(int width, int height) : base(width, height)
        {
        }
    }

    public class DefaultMapGenerator : IMapGenerator
    {
        public IMap GenerateMap(int width, int height)
        {
            IMap map = new DefaultMap(width, height);

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(x <= 1 || x >= width - 2 || y <= 1 || y >= height - 2)
                    {
                        map[x, y] = new WallTile();
                    }
                }
            }

            map.SetUpTileNeigbours();

            return map;
        }
    }
}
