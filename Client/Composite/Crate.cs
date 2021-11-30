using Client.Command;
using Client.Flyweight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Client.Composite
{
    class Crate : Item
    {
        List<Item> items = new List<Item>();
        Invoker invoker = new Invoker();
        string action = "";


        public Crate()
        {
            this.Type = this.GetType().Name;
        }

        public override void Add(Item item)
        {
            items.Add(item);
        }

        public override Item Remove(Item item)
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

        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            img.Fill = Image_brush_factory.Get_image_brush("Crate");
            return img;
        }

        public override void PickupEffect(Player p)
        {
            
        }

        public override int GetNumber()
        {
            throw new NotImplementedException();
        }
    }
}
