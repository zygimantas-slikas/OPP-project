using Client.Factory;
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
    public class VisibleTrap
    {
        private static IFactory<BitmapImage> bitmapimage;
        public int Damage = 30;

        public Rectangle get_view()
        {
            bitmapimage = new Traps();
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = bitmapimage.FactoryMethod("Trap3");
            img.Fill = myBrush;
            return img;
        }
    }
}
