using Boyd.Games.RebornMars.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Service
{
    /// <summary>
    /// This service is responsible for generating and updating monsters.
    /// </summary>
    public class MonsterService : IActiveService
    {
        public void EndService()
        {
            
        }

        public void StartService()
        {
            
        }

        public void Update()
        {
            MapState state = Game.Current.Dungeon.CurrentMapState;

            foreach(IMonster monster in state.Monsters)
            {
                monster.Update();
            }
        }
    }
}
