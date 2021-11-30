
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Composite
{
    public class Crate : Item
    {
        List<Item> items = new List<Item>();
        string action = "";


        public Crate()
        {
            this.Type = this.GetType().Name;
        }

        public override void Add(Item item, Player p)
        {
            items.Add(item);
        }

        public override Item Remove(Item item, Player p)
        {
            Item item1 = null;
            if (items.Count > 0)
            {
                item1 = items[items.Count - 1];
                items.RemoveAt(items.Count - 1);
            }
            return item1;
        }

        public List<Item> GetItems()
        {
            return items;
        }


        public override bool IsInventory() { return true; }

        public override void PickupEffect(Player p)
        {
            throw new NotImplementedException();
        }


        public override Cloneable Clone()
        {
            Crate c = new Crate();
            c.Type = this.Type;
            return c;
        }
    }
}
