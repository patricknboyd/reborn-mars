using Boyd.Games.RebornMars.Assets;
using Boyd.Games.RebornMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.World
{
    public class Dungeon
    {
        /// <summary>
        /// Gets the dungeon branch that the player is currently in.
        /// </summary>
        public IDungeonBranch CurrentBranch { get; private set; }
        
        /// <summary>
        /// Stores the collection of dungeon branches, and what dungeon level the top floor of the branch is on.
        /// </summary>
        private Dictionary<IDungeonBranch, int> _dungeonBranches;

        /// <summary>
        /// Gets the current map level.
        /// </summary>
        public MapState CurrentMapState { get; private set; }

        /// <summary>
        /// Gets the collection of dungeon branches in this game.
        /// </summary>
        public IEnumerable<IDungeonBranch> DungeonBranches
        {
            get { return _dungeonBranches.Keys; }
        }

        public int CurrentDungeonLevel { get; private set; }

        public Dungeon()
        {
            _dungeonBranches = new Dictionary<IDungeonBranch, int>();
            CurrentDungeonLevel = 1;
        }

        /// <summary>
        /// Adds a dungeon branch to the dungeon.
        /// </summary>
        /// <param name="branch">The new branch to add.</param>
        /// <param name="topLevel">The dungeon level that the top level of this branch is on.</param>
        public void AddDungeonBranch(IDungeonBranch branch, int topLevel)
        {
            _dungeonBranches.Add(branch, topLevel);
        }

        /// <summary>
        /// Sets the specified branch to be the currently occupied branch. If the branch does not exist in the dungeon,
        /// an exception will be thrown.
        /// </summary>
        /// <param name="branch"></param>
        public void SetCurrentBranch(IDungeonBranch branch, bool suppressMovementEvents = false)
        {
            if (branch != CurrentBranch)
            {
                if (DungeonBranches.Contains(branch))
                {
                    CurrentBranch = branch;
                    if (!suppressMovementEvents)
                    {
                        CurrentBranch.EnterDungeonBranch();
                    }
                }
                else
                {
                    throw new InvalidOperationException(string.Format(ErrorMessages.DungeonBranchNotFound, branch.Name));
                }
            }
        }

        public void SetCurrentMap(MapState map)
        {
            CurrentMapState = map;
        }

        public void MovePlayerToLocation(DungeonLocation location, bool suppressMovementEvents = false)
        {
            MovePlayerToLocation_Internal(null, location, suppressMovementEvents);
        }

        public void MovePlayerToLocation(Staircase sendingStairs, bool suppressMovementEvents = false)
        {
            MovePlayerToLocation_Internal(sendingStairs, null, suppressMovementEvents);
        }

        private void MovePlayerToLocation_Internal(Staircase sendingStairs, DungeonLocation location, bool suppressMovementEvents)
        {
            if (location == null)
            {
                location = sendingStairs.Destination;
            }

            // Make sure that this dungeon branch does in fact exist.
            if (DungeonBranches.Contains(location.Branch))
            {
                SetCurrentBranch(location.Branch, suppressMovementEvents);

                if (!CurrentBranch.DungeonLevelExists(location.DungeonBranchLevel))
                {
                    CurrentBranch.CreateNewLevel(location.DungeonBranchLevel);
                    if (sendingStairs != null)
                    {
                        // The map position shoud be populated on the staircase now.
                        location = sendingStairs.Destination;
                    }
                }

                CurrentMapState = CurrentBranch.GetDungeonLevel(location.DungeonBranchLevel);
                Game.Current.Player.Position = location.MapPosition;

                // Update the dungeon level.
                CurrentDungeonLevel = _dungeonBranches[CurrentBranch] + CurrentMapState.DungeonBranchIndex;
            }
            else
            {
                throw new InvalidOperationException(string.Format(ErrorMessages.DungeonBranchNotFound, location.Branch.Name));
            }
        }

        /// <summary>
        /// Sets the number of levels from the top of the dungeon the branch is.
        /// </summary>
        /// <param name="branch">The branch to update.</param>
        /// <param name="position">The number of levels from the dungeon start.</param>
        public void SetBranchPosition(IDungeonBranch branch, int position)
        {
            _dungeonBranches[branch] = position;
        }

        /// <summary>
        /// Gets the number of levels from the dungeon start of a branch.
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        public int GetBranchPosition(IDungeonBranch branch)
        {
            return _dungeonBranches[branch];
        }

    }
}
