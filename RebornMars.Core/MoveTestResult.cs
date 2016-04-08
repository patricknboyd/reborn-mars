using Boyd.Games.RebornMars.Actor;
using Boyd.Games.RebornMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{
    /// <summary>
    /// Stores the results of checking whether a move is valid.
    /// </summary>
    public class MoveTestResult
    {
        private IMapTile _tile;
        private bool _isMoveValid;
        private Position _newPosition;

        /// <summary>
        /// The tile information for the destination position, will be null if the movement is not valid.
        /// </summary>
        public IMapTile Tile { get { return _tile; } }
        /// <summary>
        /// Whether the move is a valid one or not (i.e. is there a tile there at all).
        /// </summary>
        public bool IsMoveValid { get { return _isMoveValid; } }
        /// <summary>
        /// The destination position.
        /// </summary>
        public Position NewPosition { get { return _newPosition; } }

        /// <summary>
        /// Gets or sets the monster that is occupying the destination tile. If the tile is unoccupied, this will be null.
        /// </summary>
        public IMonster Monster { get; set; }

        public MoveTestResult(IMapTile tile, Position newPosition)
        {
            _tile = tile;
            _newPosition = newPosition;

            _isMoveValid = _tile != null;
        }
    }
}
