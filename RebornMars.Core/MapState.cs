using Boyd.Games.RebornMars.Actor;
using Boyd.Games.RebornMars.Assets;
using Boyd.Games.RebornMars.Map;
using Boyd.Games.RebornMars.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Boyd.Games.RebornMars
{
    /// <summary>
    /// Represents the current state of a dungeon level. This includes the terrain, as well as all monsters and items that are in the level.
    /// </summary>
    [Serializable]
    public class MapState : ISerializable
    {
        /// <summary>
        /// Gets the terrain information.
        /// </summary>
        public IMap Map { get; private set; }

        private Player Player { get; set; }

        private MonsterGenerationService MonsterGenerator { get; set; }

        private List<IMonster> _monsters;
        /// <summary>
        /// Gets the collection of monsters (excluding the player) that are currently on this level.
        /// </summary>
        public IEnumerable<IMonster> Monsters { get { return _monsters; } }

        /// <summary>
        /// Gets or sets the position of this map in its dungeon branch.
        /// </summary>
        public int DungeonBranchIndex { get; set; }

        public MapState(IMap map)
        {
            this.Map = map;
            this.Player = Game.Current.Player;

            Staircase upStairs = Map.UpStairs.FirstOrDefault();

            DungeonBranchIndex = -1;

            _monsters = new List<IMonster>();
            MonsterGenerator = Game.Current.Services.GetService<MonsterGenerationService>();
        }

        public void UpdateMapState()
        {

        }

        public void GenerateNewMonster()
        {
            Position monsterPosition = SelectRandomWalkableTile(false);


            var newMonster = MonsterGenerator.GenerateMonster(0, monsterPosition);
            _monsters.Add(newMonster);
        }
        /// <summary>
        /// Gets the map tile that is the number of spaces in the specified direction.
        /// </summary>
        /// <param name="start">The start location.</param>
        /// <param name="dir">The direction to move.</param>
        /// <returns>The tile at the end of the movement, if movement is valid, otherwise null.</returns>
        public MoveTestResult MoveTest(Position start, MoveDirection dir)
        {
            // Get the end position.
            Position end = GetDestinaionPosition(start, dir);            

            // Check if the end position is in the map still.
            IMapTile destTile;

            if (end.X < 0 || end.X >= Map.Width || end.Y < 0 || end.Y >= Map.Height)
            {
                destTile = null;
            }
            else
            {
                destTile = Map[end];
            }

            MoveTestResult result = new MoveTestResult(destTile, end);

            // Check to see if any monsters are currently occupying that tile.
            // TODO: Looking through the entire monster collection everymove might get slow, maybe store these differently.
            result.Monster = Monsters.Where(m => m.Position.Equals(end)).FirstOrDefault();

            return result;

        }

        public void RemoveMonster(IMonster monster)
        {
            _monsters.Remove(monster);
        }

        /// <summary>
        /// Gets all of the monsters adjacent to the calling monster.
        /// </summary>
        /// <param name="monster">The monster to check around.</param>
        /// <returns>A collection of IMonster objects that are adjacent to the calling monster. If no monsters are adjacent, will return and empty collection.</returns>
        public IEnumerable<IMonster> GetAdjacentMonsters(IMonster monster)
        {
            List<IMonster> adjacent = new List<IMonster>();

            foreach (IMonster mon in Monsters)
            {
                if(ArePositionsAdjacent(monster.Position, mon.Position))
                {
                    adjacent.Add(mon);
                }
            }

            return adjacent;
        }

        /// <summary>
        /// Checks to see if two positions are adjacent, including diagonally.
        /// </summary>
        /// <param name="one">The first position.</param>
        /// <param name="two">The second position.</param>
        /// <returns>True, if the positions are adjacent, otherwise false.</returns>
        public bool ArePositionsAdjacent(Position one, Position two)
        {
            int xDiff = System.Math.Abs(one.X - two.X);
            int yDiff = System.Math.Abs(one.Y - two.Y);

            return xDiff <= 1 && yDiff <= 1;
        }


        public Position SelectRandomWalkableTile(bool allowOccupied = true)
        {
            return SelectRandomTile(Map.MapTiles.Where(t => t.IsWalkable), allowOccupied);
        }

        public Position SelectRandomFlyableTile(bool allowOccupied = true)
        {
            return SelectRandomTile(Map.MapTiles.Where(t => t.IsFlyable), allowOccupied);
        }

        public Position SelectRandomSwimmableTile(bool allowOccupied = true)
        {
            return SelectRandomTile(Map.MapTiles.Where(t => t.IsSwimmable), allowOccupied);
        }

        private Position SelectRandomTile(IEnumerable<IMapTile> tileCandidates, bool allowOccupied)
        {
            int count = tileCandidates.Count();
            Position tile = null;

            if (count > 0)
            {
                bool foundTile = false;

                while (!foundTile)
                {
                    // This has the potential for an infinite loop.
                    int randomIndex = Game.Current.RNG.Next(0, tileCandidates.Count());

                    tile = tileCandidates.ElementAt(randomIndex).Position;

                    if (allowOccupied || GetMonsterAtTile(tile) == null)
                    {
                        foundTile = true;
                    }
                }
            }

            return tile;
        }

        public void LookAtTile(Position tilePosition)
        {
            try
            {
                Game.Current.Messages.AddMessage(GetTileDescription(tilePosition));
            }
            catch(IndexOutOfRangeException)
            {
                Game.Current.Messages.AddDebugMessage("Tried to look at out of bounds position: {0}.", tilePosition.ToString());
            }
        }

        public IMonster GetMonsterAtTile(Position tilePosition)
        {
            return Monsters.Where(m => m.Position.Equals(tilePosition)).FirstOrDefault();
        }

        private string GetTileDescription(Position tilePosition)
        {
            if(tilePosition.Equals(Map.UpStairs))
            {
                return ObjectDescriptions.UpStairs;
            }
            else if(tilePosition.Equals(Map.DownStairs))
            {
                return ObjectDescriptions.DownStairs;
            }
            else
            {
                return Map[tilePosition].Description;
            }
        }

        private Position GetDestinaionPosition(Position start, MoveDirection dir)
        {
            Position end;

            switch (dir)
            {
                case MoveDirection.Left:
                    end = new Position(start.X - 1, start.Y);
                    break;
                case MoveDirection.UpLeft:
                    end = new Position(start.X - 1, start.Y + 1);
                    break;
                case MoveDirection.Up:
                    end = new Position(start.X, start.Y + 1);
                    break;
                case MoveDirection.UpRight:
                    end = new Position(start.X + 1, start.Y + 1);
                    break;
                case MoveDirection.Right:
                    end = new Position(start.X + 1, start.Y);
                    break;
                case MoveDirection.DownRight:
                    end = new Position(start.X + 1, start.Y - 1);
                    break;
                case MoveDirection.Down:
                    end = new Position(start.X, start.Y - 1);
                    break;
                case MoveDirection.DownLeft:
                    end = new Position(start.X - 1, start.Y - 1);
                    break;
                default:
                    throw new ArgumentException(string.Format(ErrorMessages.UnrecognizedEnumValue, dir.ToString()));
            }

            return end;
        }

        #region ISerializableMembers

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public MapState(SerializationInfo info, StreamingContext context)
        {

        }

        #endregion
    }
}