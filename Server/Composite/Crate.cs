
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

        public enum Crate_type { general, guns, health };
        public override Item.Crate_type Storage { get; set; }
        List<Item> items = new List<Item>();
        string action = "";


        public Crate(Item.Crate_type str = Item.Crate_type.general)
        {
            Storage = str;
            this.Type = this.GetType().Name;
        }

        public override void Add(Item item, Player p)
        {
            if (this.Storage != Item.Crate_type.general)
            {
                if ((this.Storage == Item.Crate_type.guns && (item.Type == "BlueGun" || item.Type == "RedGun")) ||
                    (this.Storage == Item.Crate_type.health && (item.Type == "BlueMedicKit" || item.Type == "RedMedicKit")))
                    items.Add(item);
            }
            else
            {
                if(item.Type == "Crate")
                {
                    items.Add(item);
                }
            }
        }

        public override Item Remove(Item item, Player p, out bool crateOfItems)
        {
            Item item1 = null;
            crateOfItems = false;
            if ((this.Storage == Item.Crate_type.guns && (item.Type == "BlueGun" || item.Type == "RedGun")) ||
                    (this.Storage == Item.Crate_type.health && (item.Type == "BlueMedicKit" || item.Type == "RedMedicKit")))
            {
                crateOfItems = true;
                return this;
            }
            else if (items.Count > 0)
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


        public override bool IsCrate() { return true; }

        public override void PickupEffect(Player p)
        {
            int i = 0;
            string result = "Items in Crate: (";

            foreach (Item component in this.items)
            {
                result += component.Type;
                if (i != this.items.Count - 1)
                {
                    result += "+";
                }
                i++;
            }

            result += ")";
            Console.WriteLine(result);
        }


        public override Cloneable Clone()
        {
            Crate c = new Crate();
            c.Type = this.Type;
            return c;
        }
    }
}
