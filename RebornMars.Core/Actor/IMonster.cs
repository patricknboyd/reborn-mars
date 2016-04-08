using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Actor
{

    public interface IMonster
    {
        Position Position { get; set; }

        string Name { get; }
        string DefinitiveForm { get; }
        string AttackVerb { get; }
        char Symbol { get; }
        MonsterMovementType Movement { get; set; }

        int MaxHP { get; set; }
        int CurrentHP { get; set; }
        int Strength { get; set; }
        int Constitution { get; set; }
        int Dexterity { get; set; }
        int Intelligence { get; set; }
        int Luck { get; set; }

        int Armour { get; set; }
        int Evasion { get; set; }

        void Update();

        AttackResult Attack(IMonster target);
        void TakeDamage(AttackResult attack);
        void Kill();
    }
}
