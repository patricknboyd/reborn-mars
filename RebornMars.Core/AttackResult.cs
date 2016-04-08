using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars
{
    /// <summary>
    /// Repesents the results of an attack on a monster.
    /// </summary>
    public class AttackResult
    {
        /// <summary>
        /// Represents an attack result that missed.
        /// </summary>
        public static AttackResult Miss
        {
            get { return new AttackResult(false, 0); }
        }

        /// <summary>
        /// If the attack successfully connected returns true, otherwise false.
        /// </summary>
        public bool IsHit { get; private set; }
        /// <summary>
        /// The amount of damage dealt to the monster.
        /// </summary>
        public int Damage { get; private set; }

        /// <summary>
        /// Creates a new AttackResult instance.
        /// </summary>
        /// <param name="isHit">Whether the attack successfully hit.</param>
        /// <param name="damage">The amount of damage done.</param>
        public AttackResult(bool isHit, int damage)
        {
            IsHit = isHit;
            Damage = damage;
        }
    }
}
