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
        public enum Crate_type { general, guns, health }
        public Crate_type Storage { get; set; }
        public Crate(Crate_type str = Crate_type.general)
        {
            this.Storage = str;
            this.Type = this.GetType().Name;
        }
        public override bool IsCrate() { return true; }

        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            if(Storage == Crate_type.guns)
            {
                img.Fill = Image_brush_factory.Get_image_brush("CrateGun");
            }
            else if(Storage == Crate_type.health)
            {
                img.Fill = Image_brush_factory.Get_image_brush("CrateMed");
            }
            else
            {
                img.Fill = Image_brush_factory.Get_image_brush("Crate");
            }
            
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
