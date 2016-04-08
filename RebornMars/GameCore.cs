using Boyd.Games.RebornMars.Actor;
using Boyd.Games.RebornMars.Assets;
using Boyd.Games.RebornMars.Map;
using Boyd.Games.RebornMars.Service;
using Boyd.Games.RebornMars.World;
using Boyd.Games.WangTiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{
    /// <summary>
    /// Represents the core game functionality. 
    /// </summary>
    public class GameCore : IGameCore
    {
        #region Constants

        private const string WangTilesDefinitionPath = @"C:\Users\patri\OneDrive\Documents\visual studio 2015\Projects\RogueWangTiles\RogueWangTiles\Resources\tiles9x9.txt";

        #endregion

        #region Constructors

        public GameCore()
        {
            Messages = new GameConsole();

            InitializeCoreServices();
            InitializeInputManager();
        }

        #endregion

        #region Events

        /// <summary>
        /// This event is raised when all of the processing for the current turn is complete.
        /// </summary>
        public event EventHandler TurnCompleted;

        private void OnTurnCompleted()
        {
            var handler = this.TurnCompleted;

            if (handler != null)
            {
                handler(this, null);
            }
        }

        /// <summary>
        /// This event is raised when the game has ended and it is trying to quit.
        /// </summary>
        public event EventHandler GameClosed;

        private void OnGameClosed()
        {
            var handler = this.GameClosed;

            if(handler != null)
            {
                handler(this, null);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the game service provider.
        /// </summary>
        public IServiceManager Services { get; private set; }

        public MapGenerationService MapGenerator { get; private set; }

        public IGameConsole Messages { get; private set; }

        public IInputManager Input { get; private set; }

        public RngService RNG { get; private set; }

        public Player Player { get; private set; }

        public Dungeon Dungeon { get; private set; }

        private List<Staircase> _unresolvedStaircases;
        public IEnumerable<Staircase> UnresolvedStaircases { get { return _unresolvedStaircases; } }

        #endregion

        #region Public Methods

        public void StartGame()
        {
            _unresolvedStaircases = new List<Staircase>();

            foreach (IService service in Services)
            {
                service.StartService();
            }

            Player = new Player(0, 0);

            CreateMapGenerators();
            CreateDungeon();

            EnterDungeon();

            OnTurnCompleted();
        }

        public bool MoveDownStairs()
        {
            Position playerPosition = Game.Current.Player.Position;
            Staircase downStairs = Dungeon.CurrentMapState.Map.DownStairs.Where(s => s.Location.MapPosition.Equals(playerPosition)).FirstOrDefault();

            if (downStairs != null)
            {
                UseStaircase(downStairs);
                return true;
            }
            else
            {
                Game.Current.Messages.AddMessage(ErrorMessages.MoveDownNotOnStairs);
                return false;
            }
        }

        public bool MoveUpStairs()
        {
            Position playerPosition = Game.Current.Player.Position;
            Staircase upStairs = Dungeon.CurrentMapState.Map.UpStairs.Where(s => s.Location.MapPosition.Equals(playerPosition)).FirstOrDefault();

            if (upStairs != null)
            {
                UseStaircase(upStairs);
                return true;
            }
            else
            {
                Game.Current.Messages.AddMessage(ErrorMessages.MoveUpNotOnStairs);
                return false;
            }
        }

        public void EndGame()
        {
            // Dispose of all of the game resources here.

            if (Services != null)
            {
                foreach (IService service in Services)
                {
                    service.EndService();
                }
            }

            OnGameClosed();
        }

        public void AddUnresolvedStaircase(Staircase stairs)
        {
            _unresolvedStaircases.Add(stairs);
        }

        public void ResolveStaircaseDestination(Staircase stairs)
        {
            _unresolvedStaircases.Remove(stairs);
        }

        #endregion

        #region Private Methods

        private void EnterDungeon()
        {
            IDungeonBranch startingBranch = Dungeon.DungeonBranches.FirstOrDefault();

            startingBranch.EnterDungeonBranch();

            Dungeon.SetCurrentMap(startingBranch.CreateNewLevel(0));

            // Generate a monster to fight for now./
            Dungeon.CurrentMapState.GenerateNewMonster();

            DungeonLocation startingLocation = new DungeonLocation(
                startingBranch,
                0,
                Dungeon.CurrentMapState.SelectRandomWalkableTile(false));

            // Place the player.
            Dungeon.MovePlayerToLocation(startingLocation, true);

        }

        private void InitializeCoreServices()
        {
            Services = new ServiceManager(this);

            // Create a map generation servivce.
            MapGenerator = new MapGenerationService();
            Services.RegisterService(MapGenerator);

            // Create an Random Number Generator service.
            RNG = new RngService();
            Services.RegisterService(RNG);

            // Create the monster generation service.
            MonsterGenerationService monsterGen = new MonsterGenerationService();
            Services.RegisterService(monsterGen);

            // Create the mosnter service.
            MonsterService monsters = new MonsterService();
            Services.RegisterService(monsters);
        }

        private void InitializeInputManager()
        {
            Input = new InputManager();

            Input.InputCompleted += InputCompleted;
        }

        private void InputCompleted(object sender, InputCompletedEventHandlerArgs args)
        {
            EndTurn();
        }

        private void EndTurn()
        {
            OnTurnCompleted();
        }

        private void CreateDungeon()
        {
            Dungeon = new Dungeon();

            IDungeonBranch testBranch = new MainDungeonBranch();

            Dungeon.AddDungeonBranch(testBranch, 1);

            IDungeonBranch debugBranch = new DebugDungeonBranch();
            Dungeon.AddDungeonBranch(debugBranch, -1);

            foreach(IDungeonBranch branch in Dungeon.DungeonBranches)
            {
                branch.CreateDungeonBranchConnections(Dungeon.DungeonBranches);
            }
        }

        private void CreateMapGenerators()
        {
            IMapGenerator wangTiles = WangTileDefinitionLoader.CreateGeneratorFromDefinitionFile(WangTilesDefinitionPath);

            MapGenerator.AddMapGenerator(wangTiles);
        }

        private void UseStaircase(Staircase stairs)
        {

            Dungeon.MovePlayerToLocation(stairs);
        }

        #endregion
    }
}
