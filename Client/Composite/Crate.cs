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
        public Crate()
        {
            this.Type = this.GetType().Name;
        }
        public override bool IsCrate() { return true; }

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
