using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Map
{
    [Serializable]
    public abstract class MapTileBase : IMapTile
    {
        public string Description { get; protected set; }

        public bool IsFlyable { get; protected set; }

        public bool IsSwimmable { get; protected set; }

        public bool IsWalkable { get; protected set; }
        
        public IMapTile Down { get; protected set; }

        public IMapTile DownLeft { get; protected set; }

        public IMapTile DownRight { get; protected set; }

        public IMapTile Left { get; protected set; }

        public Position Position { get; set; }

        public IMapTile Right { get; protected set; }

        public IMapTile Up { get; protected set; }

        public IMapTile UpLeft { get; protected set; }

        public IMapTile UpRight { get; protected set; }

        public void SetUpNeighbours(IMap map)
        {
            int x = Position.X;
            int y = Position.Y;

            bool atTop = (y == map.Height - 1);
            bool atBottom = (y == 0);
            bool atLeft = (x == 0);
            bool atRight = (x == map.Width - 1);

            // Select tiles from the row above.
            if(!atTop)
            {
                if(!atLeft)
                {
                    UpLeft = map[x - 1, y + 1];
                }
                Up = map[x, y + 1];
                if(!atRight)
                {
                    UpRight = map[x + 1, y + 1];
                }
            }

            // Select tiles from the row below.
            if(!atBottom)
            {
                if (!atLeft)
                {
                    DownLeft = map[x - 1, y - 1];
                }
                Down = map[x, y - 1];
                if (!atRight)
                {
                    DownRight = map[x + 1, y - 1];
                }
            }

            // Select left and right.

            if(!atLeft)
            {
                Left = map[x - 1, y];
            }
            if(!atRight)
            {
                Right = map[x + 1, y];
            }

        }
    }
}
