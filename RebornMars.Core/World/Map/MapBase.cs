using Boyd.Games.RebornMars.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Map
{
    // Represents the basic terrain of a map.
    public abstract class MapBase : IMap
    {
        /// <summary>
        /// Gets the width of the map.
        /// </summary>
        public int Width { get; protected set; }
        /// <summary>
        /// Gets the height of the map.
        /// </summary>
        public int Height { get; protected set; }

        protected List<Staircase> MapStairs { get; set; }
        public IEnumerable<Staircase> Stairs { get { return MapStairs; } }

        public IEnumerable<Staircase> UpStairs
        {
            get { return Stairs.Where(s => s.Direction == Staircase.StaircaseDirection.Up); }
        }

        public IEnumerable<Staircase> DownStairs
        {
            get { return Stairs.Where(s => s.Direction == Staircase.StaircaseDirection.Down); }
        }

        private Type _defaultTileType;
        /// <summary>
        /// Gets or sets the default tile type that will fill the level when a new instance is created.
        /// </summary>
        public Type DefaultTileType
        {
            get { return _defaultTileType; }
            set
            {
                if (value.GetInterface(typeof(IMapTile).Name) != null)
                {
                    _defaultTileType = value;
                }
                else
                {
                    throw new ArgumentException(string.Format(ErrorMessages.ProvidedTypeIsNotTile, value.GetType().ToString()));
                }
            }
        }

        /// <summary>
        /// Gets or sets the map tile located at the specified position.
        /// </summary>
        /// <param name="x">The x-coordinate of the tile.</param>
        /// <param name="y">The y-coordinate of the tile.</param>
        /// <returns>The map tile at that location.</returns>
        public IMapTile this[int x, int y]
        {
            get { return _mapTiles[GetTileIndex(x, y)]; }
            set
            {
                value.Position = new Position(x, y);
                _mapTiles[GetTileIndex(x, y)] = value;
            }
        }

        /// <summary>
        /// Gets or sets the map tile located at the specified position.
        /// </summary>
        /// <param name="p">The coordinates of the tile.</param>
        /// <returns>The map tile at that location.</returns>
        public IMapTile this[Position p]
        {
            get { return _mapTiles[GetTileIndex(p.X, p.Y)]; }
            set
            {
                value.Position = p;
                _mapTiles[GetTileIndex(p.X, p.Y)] = value;
            }
        }

        private List<IMapTile> _mapTiles;
        /// <summary>
        /// Gets the tiles in the current map. Map coordinates are given by [x,y] where the bottom left corner of the map is [0,0].
        /// </summary>
        public IEnumerable<IMapTile> MapTiles { get { return _mapTiles; } }

        public MapBase() : this(0, 0) { }

        public MapBase(int width, int height)
        {
            Width = width;
            Height = height;
            MapStairs = new List<Staircase>();
            DefaultTileType = typeof(FloorTile);

            InitializeTiles();
        }

        public void AddStaircase(Staircase stairs)
        {
            MapStairs.Add(stairs);
        }

        public void RemoveAllStairs()
        {
            MapStairs.Clear();
        }


        protected void InitializeTiles()
        {
            _mapTiles = new List<IMapTile>(Width * Height);

            for(int y = 0; y < Height; y++)
            {
                for(int x = 0; x < Width; x++)
                {
                    IMapTile tile = (IMapTile)Activator.CreateInstance(DefaultTileType);
                    tile.Position = new Position(x, y);

                    _mapTiles.Add(tile);
                }
            }
        }

        private int GetTileIndex(int x, int y)
        {
            return (y * Width) + x;
        }

        public void SetUpTileNeigbours()
        {
            foreach(MapTileBase tile in MapTiles)
            {
                tile.SetUpNeighbours(this);
            }
        }
    }
}
