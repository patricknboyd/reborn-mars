using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.World
{
    /// <summary>
    /// Defines a connection between two different dungeon branches. General guidelines of its location are programmed, and its actual position is determined at run time.
    /// </summary>
    public class DungeonBranchConnection
    {
        /// <summary>
        /// Gets the destination branch.
        /// </summary>
        public IDungeonBranch DestinationBranch { get; private set; }
        /// <summary>
        /// Gets whether the connection uses an upstair or a downstair.
        /// </summary>
        public Map.Staircase.StaircaseDirection StairDirection { get; private set; }
        /// <summary>
        /// Gets the first possible dungeon branch index that this branch can appear on.
        /// </summary>
        public int EarliestIndex { get; private set; }
        /// <summary>
        /// Gets the last possible index the connection can appear on.
        /// </summary>
        public int LatestIndex { get; private set; }

        public int MinimumDestinationIndex { get; private set; }
        public int MaximumDestinationIndex { get; private set; }

        public bool StaircaseCreated { get; set; }


        public DungeonBranchConnection(IDungeonBranch destination, Map.Staircase.StaircaseDirection direction, int earliestAppearance, int lastAppearance, int minDestIndex, int maxDestIndex)
        {
            DestinationBranch = destination;
            StairDirection = direction;
            EarliestIndex = earliestAppearance;
            LatestIndex = lastAppearance;
            MinimumDestinationIndex = minDestIndex;
            MaximumDestinationIndex = maxDestIndex;

            StaircaseCreated = false;
        }

    }
}
