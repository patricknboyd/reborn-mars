using Boyd.Games.RebornMars.Assets;
using Boyd.Games.RebornMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{
    internal class InputManager : IInputManager
    {
        public event EventHandler<InputCompletedEventHandlerArgs> InputCompleted;

        public void Move(MoveDirection dir)
        {
            Game.Current.Player.Move(dir);

            OnInputCompleted(1);
        }

        public void Quit()
        {
            Game.Current.EndGame();
        }

        public void LookAtTile(Position pos)
        {
            Game.Current.Dungeon.CurrentMapState.LookAtTile(pos);
            OnInputCompleted(0);
        }

        public void LookAtCurrentTile()
        {
            LookAtTile(Game.Current.Player.Position);
        }

        public void MoveUpStairs()
        {
            if(Game.Current.MoveUpStairs())
            {
                OnInputCompleted(1);
            }
            else
            {
                OnInputCompleted(0);
            }
        }

        public void MoveDownStairs()
        {
            if (Game.Current.MoveDownStairs())
            {
                OnInputCompleted(1);
            }
            else
            {
                OnInputCompleted(0);
            }
        }

        private void OnInputCompleted(int turnsPassed)
        {
            var handler = this.InputCompleted;

            if(handler != null)
            {
                handler(this, new InputCompletedEventHandlerArgs(turnsPassed));
            }
        }
    }
}
