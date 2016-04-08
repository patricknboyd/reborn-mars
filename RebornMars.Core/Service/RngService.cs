using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Service
{
    /// <summary>
    /// This service is used to generate all random numbers for the game.
    /// </summary>
    public class RngService : IPassiveService
    {
        private Random Random { get; set; }

        /// <summary>
        /// Sets the seed of the random number generator.
        /// </summary>
        /// <param name="seed">The value to seed the RNG with.</param>
        public void SetRngSeed(int seed)
        {
            Random = new Random(seed);
        }

        public void EndService()
        {
            
        }

        public void StartService()
        {
            Random = new Random();
        }

        /// <summary>
        /// Returns a random number greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            return Random.NextDouble();
        }

        /// <summary>
        /// Returns an integer within the specified range.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the range.</param>
        /// <param name="max">The exclusive upper bound of the range.</param>
        /// <returns></returns>
        public int Next(int min, int max)
        {
            return Random.Next(min, max);
        }
    }
}
