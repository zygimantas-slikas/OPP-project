using System;
using System.Windows.Media.Imaging;

namespace Client.Factory
{
    public class Traps : IFactory<BitmapImage>
    {
        public BitmapImage FactoryMethod(String s)
        {
            switch (s)
            {
                case "Trap1":
                    return new BitmapImage(new Uri(@"..\..\..\..\Sprites\trap1.png", UriKind.RelativeOrAbsolute));
                case "Trap2":
                    return new BitmapImage(new Uri(@"..\..\..\..\Sprites\trap2.png", UriKind.RelativeOrAbsolute));
                case "Trap3":
                    return new BitmapImage(new Uri(@"..\..\..\..\Sprites\trap3.png", UriKind.RelativeOrAbsolute));
                default:
                    return null;
            }
        }
    }
}
