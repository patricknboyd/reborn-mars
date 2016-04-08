using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.UI.Shell.Input
{
    /// <summary>
    /// This class handles mapping keyboard input to in-game actions.
    /// </summary>
    public class InputMapper
    {
        private Dictionary<InputKey, Action> InputMap { get; set; }
        private IInputManager InputManager { get; set; }

        public InputMapper()
        {
            InputMap = new Dictionary<InputKey, Action>();
            InputManager = Game.Current.Input;

            SetupMovementInput();
            SetupGameManagementKeys();
            SetupMapInteractionKeys();
        }

        /// <summary>
        /// Retrives the action associated with the given console key.
        /// </summary>
        /// <param name="keyInfo">The key that was pressed.</param>
        /// <returns>An action that the key is associated with. If the key has no associated action, will return null instead.</returns>
        public Action GetActionForKey(ConsoleKeyInfo keyInfo)
        {
            InputKey key = GetInputKeyFromInput(keyInfo);

            if(InputMap.ContainsKey(key))
            {
                return InputMap[key];
            }
            else
            {
                return null;
            }
        }

        private void SetupMovementInput()
        {
            // Map the arrow keys to movement.
            InputMap.Add(InputKey.LeftArrow, MoveLeft);
            InputMap.Add(InputKey.UpArrow, MoveUp);
            InputMap.Add(InputKey.RightArrow, MoveRight);
            InputMap.Add(InputKey.DownArrow, MoveDown);

            // Map the old-school rogue movement keys.
            InputMap.Add(InputKey.h, MoveLeft);
            InputMap.Add(InputKey.y, MoveUpLeft);
            InputMap.Add(InputKey.k, MoveUp);
            InputMap.Add(InputKey.u, MoveUpRight);
            InputMap.Add(InputKey.l, MoveRight);
            InputMap.Add(InputKey.n, MoveDownRight);
            InputMap.Add(InputKey.j, MoveDown);
            InputMap.Add(InputKey.b, MoveDownLeft);

            // Stair movements
            InputMap.Add(InputKey.LessThan, InputManager.MoveUpStairs);
            InputMap.Add(InputKey.GreaterThan, InputManager.MoveDownStairs);
        }

        private void SetupMapInteractionKeys()
        {
            InputMap.Add(InputKey.Period, InputManager.LookAtCurrentTile);
        }

        private void SetupGameManagementKeys()
        {
            InputMap.Add(InputKey.Q, InputManager.Quit);
        }

        private void MoveLeft() { InputManager.Move(MoveDirection.Left); }
        private void MoveUpLeft() { InputManager.Move(MoveDirection.UpLeft); }
        private void MoveUp() { InputManager.Move(MoveDirection.Up); }
        private void MoveUpRight() { InputManager.Move(MoveDirection.UpRight); }
        private void MoveRight() { InputManager.Move(MoveDirection.Right); }
        private void MoveDownRight() { InputManager.Move(MoveDirection.DownRight); }
        private void MoveDown() { InputManager.Move(MoveDirection.Down); }
        private void MoveDownLeft() { InputManager.Move(MoveDirection.DownLeft); }

        public InputKey GetInputKeyFromInput(ConsoleKeyInfo input)
        {
            int inputChar = (int)input.KeyChar;

            if(inputChar >= 32 && inputChar <= 126)
            {
                return (InputKey)inputChar;
            }

            // Check for other inputs.

            ConsoleKey key = input.Key;

            switch(key)
            {
                case ConsoleKey.LeftArrow:
                    return InputKey.LeftArrow;

                case ConsoleKey.UpArrow:
                    return InputKey.UpArrow;

                case ConsoleKey.RightArrow:
                    return InputKey.RightArrow;

                case ConsoleKey.DownArrow:
                    return InputKey.DownArrow;

                case ConsoleKey.NumPad0:
                    return InputKey.NumPad0;

                case ConsoleKey.NumPad1:
                    return InputKey.NumPad1;

                case ConsoleKey.NumPad2:
                    return InputKey.NumPad2;

                case ConsoleKey.NumPad3:
                    return InputKey.NumPad3;

                case ConsoleKey.NumPad4:
                    return InputKey.NumPad4;

                case ConsoleKey.NumPad5:
                    return InputKey.NumPad5;

                case ConsoleKey.NumPad6:
                    return InputKey.NumPad6;

                case ConsoleKey.NumPad7:
                    return InputKey.NumPad7;

                case ConsoleKey.NumPad8:
                    return InputKey.NumPad8;

                case ConsoleKey.NumPad9:
                    return InputKey.NumPad9;
            }

            return InputKey.Unrecognized;
        }
    }
}
