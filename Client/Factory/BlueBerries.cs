using System;
using System.Windows.Media.Imaging;

namespace Client.Factory
{
    public class BlueBerries : IFactory<BitmapImage>
    {
        public BitmapImage FactoryMethod(String s)
        {
            switch (s)
            {
                case "BlueBerry1":
                    return new BitmapImage(new Uri(@"..\..\..\..\Sprites\blueberry1.jpg", UriKind.RelativeOrAbsolute));
                case "BlueBerry2":
                    return new BitmapImage(new Uri(@"..\..\..\..\Sprites\blueberry2.png", UriKind.RelativeOrAbsolute));
                default:
                    return null;
            }
        }
    }
}
