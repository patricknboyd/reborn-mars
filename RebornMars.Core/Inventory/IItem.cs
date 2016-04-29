using Boyd.Games.RebornMars.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Inventory
{
    /// <summary>
    /// This interface represents all items in the game world.
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the in game description of the item.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the unique ID that identifies the item type.
        /// </summary>
        Guid ItemID { get; }

        /// <summary>
        /// Gets the type of this item.
        /// </summary>
        ItemCategory ItemType { get; }

        /// <summary>
        /// This function is called when the item is obtained by a monster.
        /// </summary>
        /// <param name="owner"></param>
        void OnItemObtained(IMonster owner);
        /// <summary>
        /// This function is called when a monster drops the item.
        /// </summary>
        /// <param name="owner"></param>
        void OnItemDropped(IMonster owner);

        /// <summary>
        /// This function is called when the item is moved over by a monster.
        /// </summary>
        /// <param name="mover"></param>
        void OnItemMoveOver(IMonster mover);
    }
}
