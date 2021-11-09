using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Decorator
{
    public class LowFireDecorator : Decorator
    {
        public virtual int Damage { get; protected set; }

        public LowFireDecorator(Item component) : base(component)
        {
            this.Damage = 10;
            this.Type = this.GetType().Name;
        }

        public override void PickupEffect(Player p)
        {
            base.PickupEffect(p);
            p.AddDamage(this.Damage);
        }

        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\lowfire.png", UriKind.RelativeOrAbsolute));
            img.Fill = myBrush;
            return img;
        }
    }
}
