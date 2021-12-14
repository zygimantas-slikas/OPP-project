using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Factory;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Mediator
{
    public class ScoreMediator : IMediator
    {
        public List<Player> players1;// = new List<Player>();
        public Dictionary<String, Shape> players_gui;//= new Dictionary<string, Shape>();
        public Canvas canvas1;
        private static IFactory<BitmapImage> bitmapimage;


        public ScoreMediator()
        {
            players1 = new List<Player>();
            players_gui = new Dictionary<string, Shape>();
            canvas1 = new Canvas();
        }
        public void AddPlayer(Player p)
        {
            if (GetPlayer(p.Name) == null){
                players1.Add(p);
            }
        }

        public Player GetPlayer(string name)
        {

            foreach(Player pl in players1)
            {
                if(pl.Name == name)
                {
                    return pl;
                }
            }
            return null;
        }

        public void Notify(Player sender, string msg)
        {
            if(msg == "update") { 
            IEnumerable<string> names = from Player p in players1 select p.Name;
            IEnumerable<string> created = players_gui.Keys.ToList();
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
                        string[] knights = { "RedKnight", "GreenKnight", "BlueKnight", "OrangeKnight" };
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
}
