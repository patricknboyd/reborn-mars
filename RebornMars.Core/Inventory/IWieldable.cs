using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Inventory
{
    public interface IWieldable : IItem
    {
        Dice Damage { get; }
    }
}
