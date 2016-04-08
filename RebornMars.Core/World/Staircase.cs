using Boyd.Games.RebornMars.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Map
{
    public class Staircase
    {
        public enum StaircaseDirection
        {
            Up,
            Down
        }

        /// <summary>
        /// Gets or sets the location of the staircase in the dungeon.
        /// </summary>
        public DungeonLocation Location { get; private set; }

        /// <summary>
        /// Gets the direction the staircase is heading.
        /// </summary>
        public StaircaseDirection Direction { get; private set; }

        /// <summary>
        /// Gets or sets the location that this staircase connects to.
        /// </summary>
        public DungeonLocation Destination { get; set; }

        public Staircase(DungeonLocation location, StaircaseDirection direction)
        {
            Location = location;
            Direction = direction;

            Destination = null;
        }
    }
}
