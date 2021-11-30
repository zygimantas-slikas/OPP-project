using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client.Flyweight
{
    public class Image_brush_factory
    {
        static protected Dictionary<string, ImageBrush> flyweights = new Dictionary<string, ImageBrush>();
        static public ImageBrush Get_image_brush(string key)
        {
            if (flyweights.ContainsKey(key))
            {
                return flyweights[key];
            }
            else
            {
                ImageBrush brush = new ImageBrush();
                switch (key)
                {
                    case "RedBerry":
                        brush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\redberry.jpg", UriKind.RelativeOrAbsolute));
                        break;
                    case "BlueBerry1":
                        brush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\blueberry1.jpg", UriKind.RelativeOrAbsolute));
                        break;
                    case "BlueBerry2":
                        brush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\blueberry2.jpg", UriKind.RelativeOrAbsolute));
                        break;
                    case "RedMedicKit":
                        brush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\redmedic.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "BlueMedicKit":
                        brush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\bluemedic.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "RedGun":
                        brush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\redgun.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "BlueGun":
                        brush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\bluegun.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "Crate":
                        brush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\crate.png", UriKind.RelativeOrAbsolute));
                        break;
                }
                flyweights.Add(key, brush);
                return brush;
            }
        }
    }
}
