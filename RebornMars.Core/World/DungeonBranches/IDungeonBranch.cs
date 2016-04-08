using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.World
{
    public interface IDungeonBranch
    {
        /// <summary>
        /// Gets the name of the dungeon branch.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets whether this branch has been visited by the player.
        /// </summary>
        bool IsBranchVisited { get; }

        /// <summary>
        /// Gets whether the branches position within the dungeon has been finalized.
        /// </summary>
        /// <remarks>
        /// The position is finalized when a staircase to this branch is generated.
        /// </remarks>
        bool PositionFinalized { get; }
        /// <summary>
        /// Gets the minimum number of levels that this dungeon branch will contain.
        /// </summary>
        int MinimumLevels { get; }
        /// <summary>
        /// Gets the maximum number of levels that this dungeon branch will contain.
        /// </summary>
        int MaximumLevels { get; }
        /// <summary>
        /// Gets the actual number of levels in this branch.
        /// </summary>
        int ActualLevels { get; }
        /// <summary>
        /// Gets the type of map generator used for this branch.
        /// </summary>
        Type MapGeneratorType { get; }
        /// <summary>
        /// Gets the collection of levels in this branch.
        /// </summary>
        IEnumerable<MapState> DungeonLevels { get; }

        IEnumerable<DungeonBranchConnection> BranchConnections { get; }

        void EnterDungeonBranch();
        MapState CreateNewLevel(int mapIndex);
        MapState GetDungeonLevel(int mapIndex);
        bool DungeonLevelExists(int mapIndex);

        void CreateDungeonBranchConnections(IEnumerable<IDungeonBranch> branches);
        void FinalizeBranchDungeonPosition(IDungeonBranch fromBranch, int fromIndex, int destIndex, Map.Staircase.StaircaseDirection stairDirection);

    }
}
