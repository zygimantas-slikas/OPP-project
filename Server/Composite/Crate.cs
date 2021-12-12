
using Server;
using Server.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Composite
{
    public class Crate : Item
    {

        public override Item.Crate_type Storage { get; set; }
        public List<Item> items = new List<Item>();


        public Crate(Item.Crate_type str = Item.Crate_type.general)
        {
            Storage = str;
            this.Type = this.GetType().Name;
        }

        public override bool Add(Item item, Player p)
        {
            bool addSuccess = false;
            if (this.Storage != Item.Crate_type.general)
            {
                if ((this.Storage == Item.Crate_type.guns && (item.Type == "BlueGun" || item.Type == "RedGun")) ||
                    (this.Storage == Item.Crate_type.health && (item.Type == "BlueMedicKit" || item.Type == "RedMedicKit")))
                {
                    items.Add(item);
                    addSuccess = true;
                }
            }
            else
            {
                if(item.Type == "Crate")
                {
                    items.Add(item);
                    addSuccess = true;
                }
            }
            return addSuccess;
        }

        public override Item Remove(Item item, Player p, out bool crateOfItems)
        {
            Item item1 = null;
            crateOfItems = false;
            if ((this.Storage == Item.Crate_type.guns || this.Storage == Item.Crate_type.health) && items.Count >= 3)
            {
                crateOfItems = true;
                return this;
            }
            else if (items.Count > 0 && this.Storage == Item.Crate_type.general) //Sell
            {
                
                PickupEffect(p);
                items = new List<Item>();
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
            p.Addpoints(this.Accept(new CrateExchangeVisitor(), 0));
        }


        public override Cloneable Clone()
        {
            Crate c = new Crate();
            c.Type = this.Type;
            return c;
        }

        public override int Accept(IVisitor<int> visitor, int state)
        {
            return visitor.VisitComposite(this, state);
        }
    }
}
