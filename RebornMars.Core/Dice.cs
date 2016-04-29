using Boyd.Games.RebornMars.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{
    /// <summary>
    /// This class represents a group of dice rolled for a randomized output for an attribute. 
    /// For example, if a weapon does '4d10 + 1' damage, this class encapsulates that.
    /// </summary>
    /// <remarks>
    /// I write gud.
    /// </remarks>
    public class Dice
    {
        /// <summary>
        /// Creates a new dice object from a string in the form (x)d(y)+(z) or (x)d(y)-(z). Capitalization is ignored.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static Dice CreateFromString(string input)
        {
            string[] tokens = input.Split(new char[] { 'd', 'D', '+', '-' }, StringSplitOptions.RemoveEmptyEntries);

            int tokenCount = tokens.Length;

            if(tokenCount != 2 || tokenCount != 3)
            {
                throw new ArgumentException(ErrorMessages.DiceInvalidFormat);
            }

            int count = Int32.Parse(tokens[0]);
            int sides = Int32.Parse(tokens[1]);
            int modifier = 0;

            if(tokenCount == 3)
            {
                modifier = Int32.Parse(tokens[2]);
                if(input.Contains('-'))
                {
                    modifier *= -1;
                }
            }

            return new Dice(count, sides, modifier);
            
        }

        /// <summary>
        /// Gets the number of dice to roll.
        /// </summary>
        public int DiceCount { get; private set; }
        /// <summary>
        /// Gets the number of sides per die.
        /// </summary>
        public int DiceSides { get; private set; }
        /// <summary>
        /// Gets the modifier applied to the dice, after they have been rolled.
        /// </summary>
        public int Modifier { get; private set; }

        /// <summary>
        /// Creates a new instance of the dice class with no modifiers.
        /// </summary>
        /// <param name="count">The number of dice to roll.</param>
        /// <param name="sides">The number of sides per die.</param>
        public Dice(int count, int sides) : this(count, sides, 0) { }

        /// <summary>
        /// Creates a new instance of the dice class.
        /// </summary>
        /// <param name="count">The number of dice to roll.</param>
        /// <param name="sides">The number of sides per die.</param>
        /// <param name="modifier">The modifier applied to the roll result.</param>
        public Dice(int count, int sides, int modifier)
        {
            DiceCount = count;
            DiceSides = sides;
            Modifier = modifier;
        }

        /// <summary>
        /// Rolls the dice and returns the result.
        /// </summary>
        /// <returns></returns>
        public int Roll()
        {
            int result = 0;
            var rng = Game.Current.RNG;

            for(int i = 0; i < DiceCount; i++)
            {
                result += rng.Next(1, DiceSides + 1);
            }

            return result + Modifier;
        }
    }
}
