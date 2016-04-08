using Boyd.Games.RebornMars.Actor;
using Boyd.Games.RebornMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.UI.Shell.Output
{
    public class MapStateOutput
    {
        private MapTileOutput tileOutput;

        public MapStateOutput()
        {
            tileOutput = new MapTileOutput();
        }

        public string GetMapStateOutput(IGameCore game)
        {
            MapState mapState = game.Dungeon.CurrentMapState;

            char[,] output = new char[mapState.Map.Width, mapState.Map.Height];

            OutputBasicTerrain(mapState.Map, output);
            OutputStairs(mapState.Map, output);
            OutputMonsters(game, output);

            return BuildOutputString(output, mapState.Map.Width, mapState.Map.Height);
        }

        private void OutputBasicTerrain(IMap map, char[,] output)
        {
            for (int y = map.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    output[x, y] = tileOutput.GetTileSymbol(map, x, y);
                }
            }
        }

        private void OutputMonsters(IGameCore game, char[,] output)
        {
            if(game.Player != null)
            {
                output[game.Player.Position.X, game.Player.Position.Y] = game.Player.Symbol;
            }

            foreach(IMonster m in game.Dungeon.CurrentMapState.Monsters)
            {
                output[m.Position.X, m.Position.Y] = m.Symbol;
            }
        }

        private void OutputStairs(IMap map, char[,] output)
        {
            foreach(Staircase s in map.Stairs)
            {
                output[s.Location.MapPosition.X, s.Location.MapPosition.Y] = s.Direction == Staircase.StaircaseDirection.Up ? MapTileOutput.UpStairsSymbol : MapTileOutput.DownStairsSymbol;
            }
        }

        private string BuildOutputString(char[,] map, int width, int height)
        {
            StringBuilder outputString = new StringBuilder(width * height);

            for(int y = height -1; y >= 0; y--)
            {
                for(int x = 0; x < width; x++)
                {
                    outputString.Append(map[x, y]);
                }

                outputString.AppendLine();
            }
            return outputString.ToString();

        }
    }
}
