using System;
using System.Windows.Media.Imaging;

namespace Client.Factory
{
    public class Knight : IFactory<BitmapImage>
    {
        public BitmapImage FactoryMethod(String s)
        {
            switch (s)
            {
                case "RedKnight":
                    return new BitmapImage(new Uri(@"..\..\..\..\Sprites\knight1.png", UriKind.RelativeOrAbsolute));
                case "OrangeKnight":
                    return new BitmapImage(new Uri(@"..\..\..\..\Sprites\knight3.png", UriKind.RelativeOrAbsolute));
                case "BlueKnight":
                    return new BitmapImage(new Uri(@"..\..\..\..\Sprites\knight2.png", UriKind.RelativeOrAbsolute));
                default:
                    return null;
            }
        }
    }
}
