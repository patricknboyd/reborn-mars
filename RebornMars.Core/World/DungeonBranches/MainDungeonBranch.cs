using Boyd.Games.RebornMars.Assets;
using Boyd.Games.WangTiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.World
{
    public class MainDungeonBranch : DungeonBranchBase
    {
        public MainDungeonBranch()
            : base()
        {
            Name = "the Dungeons of Doom";
            MinimumLevels = 7;
            MaximumLevels = 10;
            MapGeneratorType = typeof(WangTilesMapGenerator);

            // This is the starting branch. Its position is known.
            PositionFinalized = true;
        }

        public override void CreateDungeonBranchConnections(IEnumerable<IDungeonBranch> branches)
        {
            base.CreateDungeonBranchConnections(branches);

            IDungeonBranch debugBranch = branches.Where(b => b is DebugDungeonBranch).FirstOrDefault();

            if(debugBranch != null)
            {
                DungeonBranchConnection connection = new DungeonBranchConnection(
                    debugBranch,
                    Map.Staircase.StaircaseDirection.Down,
                    1,
                    3,
                    0,
                    3
                    );

                AddBranchConnection(connection);
            }
            else
            {
                Game.Current.Messages.AddDebugMessage(ErrorMessages.MissingBranchForConnection, "DebugDungeonBranch");
            }
        }
    }
}
