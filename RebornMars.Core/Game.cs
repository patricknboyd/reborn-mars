using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{
    public static class Game
    {
        public static IGameCore Current { get; private set; }

        public static void StartGame(IGameCore newGame)
        {
            if(Current != null)
            {
                Current.EndGame();
            }

            Current = newGame;

        }
    }
}
