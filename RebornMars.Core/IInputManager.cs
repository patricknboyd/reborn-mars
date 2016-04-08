using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{
    public class InputCompletedEventHandlerArgs
    {
        public bool HasTimePassed { get; private set; }
        public int TurnsUsed { get; private set; }

        public InputCompletedEventHandlerArgs(int turnsUsed)
        {
            TurnsUsed = turnsUsed;
            HasTimePassed = TurnsUsed > 0;
        }
    }

    public interface IInputManager
    {
        /// <summary>
        /// Attempts to move the player character in the indicated direction.
        /// </summary>
        /// <param name="dir"></param>
        void Move(MoveDirection dir);
        /// <summary>
        /// Looks at the specified tile and returns the description to the player.
        /// </summary>
        /// <param name="pos"></param>
        void LookAtTile(Position pos);
        /// <summary>
        /// Looks at the tile that the player character is currently inhabiting.
        /// </summary>
        void LookAtCurrentTile();
        /// <summary>
        /// Attempts to quit the game.
        /// </summary>
        void Quit();

        void MoveUpStairs();

        void MoveDownStairs();

        /// <summary>
        /// This event is raised when the currently executing input action is completed.
        /// </summary>
        event EventHandler<InputCompletedEventHandlerArgs> InputCompleted;
    }
}
