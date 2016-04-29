using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boyd.Games.RebornMars.Actor;

namespace Boyd.Games.RebornMars.Inventory
{
    public class Item : IItem
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public Guid ItemID { get; protected set; }
        public ItemCategory ItemType { get; protected set; }

        public Item() : this("<Undefined>", Guid.Empty, ItemCategory.Undefined, "<Undefined item>") { }

        public Item(string name, Guid id, ItemCategory category, string description)
        {

        }

        public  virtual void OnItemDropped(IMonster owner)
        {
        }

        public virtual void OnItemMoveOver(IMonster mover)
        {
        }

        public virtual void OnItemObtained(IMonster owner)
        {
        }

        public bool Equals(Item other)
        {
            return other != null && other.ItemID == ItemID;
        }

        public override bool Equals(object obj)
        {
            Item other = obj as Item;

            if(other == null)
            {
                return false;
            }
            else
            {
                return this.Equals(other);
            }
        }

        public override int GetHashCode()
        {
            return ItemID.GetHashCode();
        }
    }
}
