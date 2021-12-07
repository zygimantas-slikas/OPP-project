﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client.Factory;

namespace Client.Observer
{
    public class Score : IScore
    {
        private static IFactory<BitmapImage> bitmapimage;
        public void Update(IEnumerable<string> names, IEnumerable<string> created, List<Player> players1, Dictionary<String, Shape> players_gui, Canvas canvas1)
        {
            foreach (var existing in created)
            {

                if (!names.Contains(existing))//delete old
                {
                    canvas1.Children.Remove(players_gui[existing]);
                    players_gui.Remove(existing);
                }
            }
            foreach (var new1 in names)
            {
                if (!created.Contains(new1))
                {
                    bitmapimage = new Knight();
                    string[] knights = {"RedKnight","GreenKnight","BlueKnight","OrangeKnight"};
                    Ellipse playerSprite = new Ellipse();
                    playerSprite.Width = 40;
                    playerSprite.Height = 40;
                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource = bitmapimage.FactoryMethod(knights[players1.Find(x => x.Name == new1).Color]);
                    playerSprite.Fill = myBrush;
                    players_gui.Add(new1, playerSprite);
                    canvas1.Children.Add(playerSprite);
                    Canvas.SetZIndex(playerSprite, 10);
                    Canvas.SetTop(playerSprite, players1.Find(x => x.Name == new1).Y * 50 + 5);
                    Canvas.SetLeft(playerSprite, players1.Find(x => x.Name == new1).X * 50 + 5);
                }
                else
                {
                    Canvas.SetTop(players_gui[new1], players1.Find(x => x.Name == new1).Y * 50 + 5);
                    Canvas.SetLeft(players_gui[new1], players1.Find(x => x.Name == new1).X * 50 + 5);
                    Canvas.SetZIndex(players_gui[new1], 3);
                }
            }
        }
    }
}
