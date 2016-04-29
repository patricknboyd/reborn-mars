using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boyd.Games.RebornMars.Actor;

namespace Boyd.Games.RebornMars.Inventory
{
    public class SwordItem : Item, IWieldable
    {
        public Dice Damage { get; protected set; }

        public SwordItem() 
            : base()
        {
            Name = "Sword";
            ItemType = ItemCategory.Weapon;
            Damage = new Dice(0, 0, 0);
        }
    }
}
