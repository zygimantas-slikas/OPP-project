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
    public class InvisibleTrap
    {
        public int Damage = 35;
        public Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\invisibletrap.png", UriKind.RelativeOrAbsolute));
            img.Fill = myBrush;
            return img;
        }
    }
}
