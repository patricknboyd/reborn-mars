using Boyd.Games.RebornMars.Actor;
using Boyd.Games.RebornMars.Map;
using Boyd.Games.RebornMars.Service;
using Boyd.Games.RebornMars.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{
    public interface IGameCore
    {
        // Events

        event EventHandler TurnCompleted;
        event EventHandler GameClosed;

        // Properties

        IServiceManager Services { get; }
        MapGenerationService MapGenerator { get; }
        IGameConsole Messages { get; }
        IInputManager Input { get; }
        RngService RNG { get; }
        Player Player { get; }
        Dungeon Dungeon { get; }
        /// <summary>
        /// Gets a collection of staircases that point at levels that haven't been created yet.
        /// </summary>
        IEnumerable<Staircase> UnresolvedStaircases { get; }

        // Methods

        void StartGame();
        void EndGame();
        bool MoveDownStairs();
        bool MoveUpStairs();
        void ResolveStaircaseDestination(Staircase stairs);
        void AddUnresolvedStaircase(Staircase stairs);

        
    }
}
