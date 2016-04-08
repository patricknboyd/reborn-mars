using Boyd.Games.RebornMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.UI.Shell.Output
{
    public class MapTileOutput
    {
        public const char FloorTileSymbol = '.';
        public static readonly char[] WallTileSymbols = new char[] {'-', '|', ' ' };
        public const char UpStairsSymbol = '<';
        public const char DownStairsSymbol = '>';

        public const char TileNotFoundSymbol = '?';

        public char GetTileSymbol(IMap map, Position pos)
        {
            return GetTileSymbol(map, pos.X, pos.Y);   
        }

        public char GetTileSymbol(IMap map, int x, int y)
        {
            IMapTile tile = map[x, y];

            if (tile is FloorTile)
            {
                return FloorTileSymbol;
            }
            else if (tile is WallTile)
            {
                return GetWallTile(map, tile);
            }
            else
            {
                return TileNotFoundSymbol;
            }
        }

        private char GetWallTile(IMap map, IMapTile tile)
        {
            // TODO: This doesn't really change for a given tile. Do we need to do this for every tile on every frame?

            bool leftWall = tile.Left == null || tile.Left is WallTile;
            bool upLeftWall = tile.UpLeft == null || tile.UpLeft is WallTile;
            bool upWall = tile.Up == null || tile.Up is WallTile;
            bool upRightWall = tile.UpRight == null || tile.UpRight is WallTile;
            bool rightWall = tile.Right == null || tile.Right is WallTile;
            bool downRightWall = tile.DownRight == null || tile.DownRight is WallTile;
            bool downWall = tile.Down == null || tile.Down is WallTile;
            bool downLeftWall = tile.DownLeft == null || tile.DownLeft is WallTile;

            if(!leftWall || !rightWall)
            {
                return WallTileSymbols[1];
            }
            else if(leftWall && upLeftWall && upWall && upRightWall && rightWall && downRightWall && downWall && downLeftWall)
            {
                return WallTileSymbols[2];
            }
            else
            {
                return WallTileSymbols[0];
            }
        }
    }
}
