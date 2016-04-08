using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{

    /// <summary>
    /// Represents how the monster gets around.
    /// </summary>
    public enum MonsterMovementType
    {
        /// <summary>
        /// The monster moves along the ground, cannot go over walls, water, lava etc.
        /// </summary>
        Walk,
        /// <summary>
        /// The monster flies, can go over water, and low walls I guess?
        /// </summary>
        Fly,
        /// <summary>
        /// The monster swims, can move in water, but not out of it.
        /// </summary>
        Swim,
        /// <summary>
        /// The monster can't/won't move.
        /// </summary>
        None
    }

    public enum MoveDirection
    {
        Left,
        UpLeft,
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft
    }
}
