using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Adapter
{
    public class InvisibleTrapAdapter : Item
    {
        private readonly InvisibleTrap _adaptee;
        public int Damage;

        public InvisibleTrapAdapter(InvisibleTrap adaptee)
        {
            this._adaptee = adaptee;
            this.Type = _adaptee.GetType().Name;
        }

        public override int GetNumber()
        {
            throw new NotImplementedException();
        }

        public override Rectangle get_view()
        {
            return this._adaptee.get_view();
        }

        public override void PickupEffect(Player p)
        {
            p.AddDamage(Damage);
        }
    }
}
