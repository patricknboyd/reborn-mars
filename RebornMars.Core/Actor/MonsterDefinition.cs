using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Actor
{
    /// <summary>
    /// This class contains the definition of a monster type.
    /// </summary>
    internal class MonsterDefinition
    {
        internal string Name { get; set; }
        internal string DefinitiveForm { get; set; }
        internal string AttackVerb { get; set; }
        internal char Symbol { get; set; }

        internal MonsterMovementType Movement { get; set; }

        internal int MaxHP { get; set; }
        internal int MaxMP { get; set; }
        internal int Strength { get; set; }
        internal int Constitution { get; set; }
        internal int Dexterity { get; set; }
        internal int Intelligence { get; set; }
        internal int Luck { get; set; }

        internal int Armour { get; set; }
        internal int Evasion { get; set; }
    }
}
