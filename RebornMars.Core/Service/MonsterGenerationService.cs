using Boyd.Games.RebornMars.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Service
{
    public class MonsterGenerationService : IPassiveService
    {
        internal Dictionary<int, MonsterDefinition> AvailableMonsters { get; private set; }

        public void EndService()
        {
            
        }

        public void StartService()
        {
            LoadMonsterDefinitions();
        }

        public IMonster GenerateMonster(int difficulty, Position pos)
        {
            // TODO: This is a debug version, it always generates the same monster.
            return new Monster(AvailableMonsters.First().Value, pos);
        }

        private void LoadMonsterDefinitions()
        {
            AvailableMonsters = new Dictionary<int, MonsterDefinition>();

            MonsterDefinition goblinDefinition = new MonsterDefinition()
            {
                Name = "goblin",
                DefinitiveForm = "the goblin",
                AttackVerb = "hits",
                Symbol = Assets.MonsterSymbols.Goblins[0],
                Movement = MonsterMovementType.Walk,

                MaxHP = 24,
                MaxMP = 0,
                Strength = 7,
                Constitution = 7,
                Dexterity = 7,
                Intelligence = 7,
                Luck = 7,
                
                Armour = 0,
                Evasion = 0
            };

            AvailableMonsters.Add(0, goblinDefinition);
        }
    }
}
