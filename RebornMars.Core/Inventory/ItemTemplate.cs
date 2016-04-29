using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Inventory
{

    public class ItemTemplate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ID { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
    }
}
