using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{
    /// <summary>
    /// A pair of coordinates representing a location on the dungeon map.
    /// </summary>
    [Serializable]
    public class Position
    {
        private int _x, _y;

        /// <summary>
        /// Gets the x coordinate of this position.
        /// </summary>
        public int X {  get { return _x; } }
        /// <summary>
        /// Gets the y coordinate of this position.
        /// </summary>
        public int Y {  get { return _y; } }
    
        /// <summary>
        /// Creates a new instance of the Position structure.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 * X;
                hash = hash * 23 * Y;

                return hash;
            }
        }
    }

}
