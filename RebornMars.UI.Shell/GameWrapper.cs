using Boyd.Games.RebornMars.UI.Shell.Input;
using Boyd.Games.RebornMars.UI.Shell.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.UI.Shell
{
    public class GameWrapper
    {
        public IGameCore CurrentGame { get; private set; }
        public InputMapper InputMap { get; private set; }

        private MapStateOutput mapStateOutput;
        private GameStateOutput gameStateOutput;
        private MessageOutput messageOutput; 
        private bool gameRunning = false;

        public GameWrapper()
        {
        }

        public void Run()
        {
            CurrentGame = new GameCore();

            Game.StartGame(CurrentGame);

            CurrentGame.TurnCompleted += Game_TurnCompleted;
            CurrentGame.GameClosed += Game_GameClosed;

            mapStateOutput = new MapStateOutput();
            messageOutput = new MessageOutput(Console.BufferWidth);
            gameStateOutput = new GameStateOutput();
            InputMap = new InputMapper();

            CurrentGame.StartGame();
            gameRunning = true;

            while(gameRunning)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                var action = InputMap.GetActionForKey(input);

                if(action != null)
                {
                    action();
                }                         
            }
        }

        private void Game_GameClosed(object sender, EventArgs e)
        {
            gameRunning = false;
        }

        private void OutputGameState()
        {
            Console.Clear();

            string message = messageOutput.GetOutputMessageLine();
            string mapOutput = mapStateOutput.GetMapStateOutput(CurrentGame);
            string gameState = gameStateOutput.OutputGameState(CurrentGame);

            Console.WriteLine(message);
            Console.Write(mapOutput);
            Console.Write(gameState);

            while (messageOutput.Messages.HasNewMessages)
            {
                Console.ReadLine();

                Console.Clear();

                message = messageOutput.GetOutputMessageLine();
                Console.WriteLine(message);
                Console.Write(mapOutput);
                Console.Write(gameState);
            }
        }

        private void Game_TurnCompleted(object sender, EventArgs e)
        {
            OutputGameState();   
        }
    }
}
