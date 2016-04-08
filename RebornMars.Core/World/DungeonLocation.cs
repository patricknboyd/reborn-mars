using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.World
{
    /// <summary>
    /// This class represents an exact position in the entire dungeon.
    /// </summary>
    public class DungeonLocation
    {
        /// <summary>
        /// Gets the dungeon branch location.
        /// </summary>
        public IDungeonBranch Branch { get; private set; }
        /// <summary>
        /// Gets the level index within the dungeon branch.
        /// </summary>
        public int DungeonBranchLevel { get; private set; }
        /// <summary>
        /// Gets the position on the level.
        /// </summary>
        public Position MapPosition { get; private set; }

        public DungeonLocation(IDungeonBranch branch, int levelIndex, Position position)
        {
            Branch = branch;
            DungeonBranchLevel = levelIndex;
            MapPosition = position;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} - {2}", Branch.Name, DungeonBranchLevel, MapPosition.ToString());
        }

        public bool Equals(DungeonLocation other)
        {
            if(other != null)
            {
                return other.Branch == this.Branch && other.DungeonBranchLevel == this.DungeonBranchLevel && this.MapPosition.Equals(this.MapPosition);
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            DungeonLocation other = obj as DungeonLocation;

            if(other != null)
            {
                return this.Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 * Branch.GetHashCode();
                hash = hash * 23 * DungeonBranchLevel.GetHashCode();
                hash = hash * 23 * MapPosition.GetHashCode();

                return hash;
            }
        }
    }
}
