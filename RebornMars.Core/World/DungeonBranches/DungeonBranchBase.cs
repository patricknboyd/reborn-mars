using Boyd.Games.RebornMars.Assets;
using Boyd.Games.RebornMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.World
{
    /// <summary>
    /// Represents a distinct section of the dungeon with its own map/monster generation rules.
    /// </summary>
    public abstract class DungeonBranchBase : IDungeonBranch
    {
        public string Name { get; protected set; }

        public bool IsBranchVisited { get; protected set; }
        public bool PositionFinalized { get; protected set; }

        public int MinimumLevels { get; protected set; }
        public int MaximumLevels { get; protected set; }
        
        public int ActualLevels { get; protected set; }

        protected Type _mapGeneratorType;
        public Type MapGeneratorType
        {
            get { return _mapGeneratorType; }
            protected set
            {
                if(value.GetInterface("Boyd.Games.RebornMars.Map.IMapGenerator") != null)
                {
                    _mapGeneratorType = value;
                }
                else
                {
                    throw new ArgumentException("MapGeneratorType value must implement Boyd.Games.RebornMars.Map.IMapGenerator.");
                }
            }
        }

        protected List<MapState> _dungeonLevels;
        public IEnumerable<MapState> DungeonLevels
        {
            get { return _dungeonLevels; }
        }

        private List<DungeonBranchConnection> _connections;
        public IEnumerable<DungeonBranchConnection> BranchConnections {  get { return _connections; } }

        protected Service.MapGenerationService MapGenerationService { get; set; }

        public DungeonBranchBase()
        {
            Name = "Undefined Dungeon Branch";
            MinimumLevels = 1;
            MaximumLevels = 1;
            MapGeneratorType = typeof(DefaultMapGenerator);

            IsBranchVisited = false;
        }

        public void EnterDungeonBranch()
        {
            if (!IsBranchVisited)
            {
                // Intitialize this dungeon branch.
                MapGenerationService = Game.Current.MapGenerator;
                ActualLevels = Game.Current.RNG.Next(MinimumLevels, MaximumLevels + 1);

                _dungeonLevels = new List<MapState>(ActualLevels);

                for (int i = 0; i < ActualLevels; i++)
                {
                    _dungeonLevels.Add(null);
                }

                // Register this branches map generator type with the map generation service, if it does not already exist.
                if (!MapGenerationService.ContainsMapGeneratorOfType(MapGeneratorType))
                {
                    MapGenerationService.AddMapGeneratorType(MapGeneratorType);
                }
            }

            Game.Current.Messages.AddMessage(ObjectDescriptions.DungeonWelcomeMessage, Name);
            IsBranchVisited = true;

        }

        /// <summary>
        /// Creates a new level for the dungeon, below the last explored level.
        /// </summary>
        /// <returns></returns>
        public MapState CreateNewLevel(int mapIndex)
        {
            MapState newLevel = MapGenerationService.GenerateNewMap(MapGeneratorType);
            newLevel.DungeonBranchIndex = mapIndex;
            PlaceNewLevelStairs(newLevel, mapIndex);

            _dungeonLevels[mapIndex] = newLevel;

            return newLevel;
        }

        /// <summary>
        /// Gets the dungeon level for the given dungeon branch level index. If no level exists at that point yet, an exception will be thrown.
        /// </summary>
        /// <param name="levelIndex"></param>
        /// <returns></returns>
        public MapState GetDungeonLevel(int levelIndex)
        {
            if(_dungeonLevels[levelIndex] == null)
            {
                throw new InvalidOperationException(string.Format(ErrorMessages.DungeonLevelDoesNotExist, levelIndex, Name));
            }

            return _dungeonLevels[levelIndex];
        }

        /// <summary>
        /// Checks to see whether the specified dungeon level has been generated yet.
        /// </summary>
        /// <param name="levelIndex">The index of the level in the dungeon branch.</param>
        /// <returns>true if the level has been generated, otherwise false.</returns>
        public bool DungeonLevelExists(int levelIndex)
        {
            return _dungeonLevels[levelIndex] != null;
        }

        public virtual void CreateDungeonBranchConnections(IEnumerable<IDungeonBranch> branches)
        {
            _connections = new List<DungeonBranchConnection>();
        }



        public void FinalizeBranchDungeonPosition(IDungeonBranch fromBranch, int fromIndex, int destIndex, Map.Staircase.StaircaseDirection stairDirection)
        {
            int indexDiff = fromIndex - destIndex + (stairDirection == Staircase.StaircaseDirection.Down ? 1 : -1);

            int fromPosition = Game.Current.Dungeon.GetBranchPosition(fromBranch);

            Game.Current.Dungeon.SetBranchPosition(this, fromPosition + indexDiff);

            PositionFinalized = true;
        }

        protected void AddBranchConnection(DungeonBranchConnection branchConnection)
        {
            _connections.Add(branchConnection);
        }

        protected void PlaceNewLevelStairs(MapState mapState, int mapIndex)
        {
            IMap map = mapState.Map;

            // Map generatos might try to place stairs, but they're not correct.
            if(map.Stairs.Count() > 0)
            {
                map.RemoveAllStairs();
            }

            // We need a list of staircases we have resolved, since we can't resolve them in this loop.
            List<Staircase> resolvedStaircases = new List<Staircase>();

            // Grab any unresolved stairs that are pointed at this level.
            foreach(Staircase unresolvedStair in Game.Current.UnresolvedStaircases.Where(s => s.Destination.Branch == this && s.Destination.DungeonBranchLevel == mapIndex))
            {
                // Create a new staircase that links to the unresolved staircase.
                Staircase newStair = CreateStaircase(mapState, mapIndex, unresolvedStair.Direction == Staircase.StaircaseDirection.Down ? Staircase.StaircaseDirection.Up : Staircase.StaircaseDirection.Down);
                unresolvedStair.Destination = newStair.Location;
                resolvedStaircases.Add(unresolvedStair);

                newStair.Destination = unresolvedStair.Location;
                mapState.Map.AddStaircase(newStair);

            }

            // Now mark them all as resolved.
            foreach (Staircase resolvedStair in resolvedStaircases)
            {
                Game.Current.ResolveStaircaseDestination(resolvedStair);
            }

            // See if there are any dungeon connections we need to create.
            foreach(var connection in BranchConnections.Where(c => !c.StaircaseCreated && mapIndex >= c.EarliestIndex && mapIndex <= c.LatestIndex))
            {
                if(ShouldCreateConnectionStaircase(connection.EarliestIndex, connection.LatestIndex, mapIndex))
                {

                    Staircase branchStair = CreateStaircase(mapState, mapIndex, connection.StairDirection);
                    int destinationLevel = Game.Current.RNG.Next(connection.MinimumDestinationIndex, connection.MaximumDestinationIndex + 1);
                    branchStair.Destination = new DungeonLocation(connection.DestinationBranch, destinationLevel, null);

                    Game.Current.AddUnresolvedStaircase(branchStair);
                    mapState.Map.AddStaircase(branchStair);

                    if (!connection.DestinationBranch.PositionFinalized)
                    {
                        connection.DestinationBranch.FinalizeBranchDungeonPosition(this, mapIndex, destinationLevel, connection.StairDirection);
                    }

                    connection.StaircaseCreated = true;
                }
            }

            // If this isn't the bottom level, and no downstairs have been created yet, we need some down stairs.
            if (mapIndex < ActualLevels - 1 && map.DownStairs.Where(s => s.Destination.Branch == this).Count() <= 0)
            {
                Staircase downStair = CreateStaircase(mapState, mapIndex, Staircase.StaircaseDirection.Down);
                downStair.Destination = new DungeonLocation(this, mapIndex + 1, null);

                Game.Current.AddUnresolvedStaircase(downStair);

                map.AddStaircase(downStair);
            }

            // If this isn't the top level, and no upstairs have been created yet, we need some up stairs.
            if (mapIndex > 0 && map.UpStairs.Where(s => s.Destination.Branch == this).Count() <= 0)
            {
                Staircase upStair = CreateStaircase(mapState, mapIndex, Staircase.StaircaseDirection.Up);
                upStair.Destination = new DungeonLocation(this, mapIndex - 1, null);

                Game.Current.AddUnresolvedStaircase(upStair);

                map.AddStaircase(upStair);
            }
        }

        protected Staircase CreateStaircase(MapState state, int mapIndex, Staircase.StaircaseDirection stairDirection)
        {
            bool foundPosition = false;
            Position stairPosition = null;

            // We need to make sure that we aren't spawning staircases on top of each other.
            while(!foundPosition)
            {
                stairPosition = state.SelectRandomWalkableTile(true);

                if(state.Map.Stairs.Where(s=>s.Location.Equals(stairPosition)).FirstOrDefault() == null)
                {
                    foundPosition = true;
                }
            }

            DungeonLocation location = new DungeonLocation(this, mapIndex, stairPosition);

            return new Staircase(location, stairDirection);
        }

        protected bool ShouldCreateConnectionStaircase(int minLevel, int maxLevel, int currentLevel)
        {
            if(currentLevel < minLevel || currentLevel > maxLevel)
            {
                throw new InvalidOperationException(ErrorMessages.CurrentLevelNotInAvailableRange);
            }

            int possibleLevelsRemaining = maxLevel - currentLevel + 1;

            if(possibleLevelsRemaining  == 1)
            {
                return true;
            }
            else
            {
                double creationOdds = 1.0 / (double)possibleLevelsRemaining;

                return Game.Current.RNG.NextDouble() < creationOdds;
            }
        }


       
    }
}
