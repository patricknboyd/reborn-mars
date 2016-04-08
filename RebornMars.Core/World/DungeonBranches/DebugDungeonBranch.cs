using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.World
{
    public class DebugDungeonBranch : DungeonBranchBase
    {
        public DebugDungeonBranch() : base()
        {
            Name = "the Debugging Branch";
            MinimumLevels = 5;
            MaximumLevels = 5;
        }
    }
}
