using Boyd.Games.RebornMars.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Inventory
{
    /// <summary>
    /// This class stores all the item types that are in the game.
    /// </summary>
    public class ItemList
    {
        public static ItemList CreateItemListFromJsonFile(string filepath)
        {
            throw new NotImplementedException();
        }

        private List<IItem> _items;
        /// <summary>
        /// Gets the collection of item types in the game. 
        /// </summary>
        public IEnumerable<IItem> Items {  get { return _items; } }

        public void AddItem(IItem item)
        {
            if(Items.Where(i => i.ItemID.Equals(item.ItemID)).Count() <= 0)
            {
                _items.Add(item);
            }
            else
            {
                throw new ArgumentException(string.Format(ErrorMessages.DuplicateItemId, item.ItemID));
            }
        }

        private ItemList()
        {
            _items = new List<IItem>();
        }
    }
}
