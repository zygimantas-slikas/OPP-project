using Client.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Client.Proxy
{
    public class MapDrawer : IMapControl
    {
        public MapDrawer(ScrollViewer sc, Player p) : base(sc, p)
        {

        }
        public override void DrawMap(Map map1, ScoreMediator m)
        {
            int pos_x = 0, pos_y = 0;
            m.canvas1.Children.Clear();
            m.canvas1.Height = Convert.ToInt32(map1.map_size) * 50 + 20;
            m.canvas1.Width = Convert.ToInt32(map1.map_size) * 50 + 20;
            for (int i = 0; i < Convert.ToInt32(map1.map_size); i++)
            {
                pos_x = 0;
                for (int j = 0; j < Convert.ToInt32(map1.map_size); j++)
                {
                    Rectangle myRect = new System.Windows.Shapes.Rectangle();
                    myRect.Stroke = System.Windows.Media.Brushes.Black;
                    if (map1.map[i, j].Surface == Tile.Tile_type.grass)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.LightGreen;
                    }
                    else if (map1.map[i, j].Surface == Tile.Tile_type.wall)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.Gray;
                    }
                    else if (map1.map[i, j].Surface == Tile.Tile_type.water)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.Blue;
                    }
                    else if (map1.map[i, j].Surface == Tile.Tile_type.lava)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.OrangeRed;
                    }
                    else if (map1.map[i, j].Surface == Tile.Tile_type.bush)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.DarkGreen;
                    }
                    myRect.Height = 50;
                    myRect.Width = 50;
                    m.canvas1.Children.Add(myRect);
                    Canvas.SetTop(myRect, pos_y);
                    Canvas.SetLeft(myRect, pos_x);
                    Canvas.SetZIndex(myRect, 0);
                    if (map1.map[i, j].Surface == Tile.Tile_type.bush)
                    {
                        Canvas.SetZIndex(myRect, 4);
                    }
                    if (map1.map[i, j].Loot != null)
                    {
                        map1.map[i, j].Icon = map1.map[i, j].Loot.get_view();
                        m.canvas1.Children.Add(map1.map[i, j].Icon);
                        Canvas.SetTop(map1.map[i, j].Icon, pos_y + 5);
                        Canvas.SetLeft(map1.map[i, j].Icon, pos_x + 5);
                        Canvas.SetZIndex(map1.map[i, j].Icon, 2);
                    }
                    pos_x += 50;
                }
                pos_y += 50;
            }
            foreach (var el in m.players_gui.Values)
            {
                m.canvas1.Children.Remove(el);
                m.canvas1.Children.Add(el);
                Canvas.SetZIndex(el, 3);
            }
            this.SetScrollBar(m);
        }
        public override void DrawTile(Map map1, ScoreMediator m, int x, int y)
        {
            Rectangle myRect = new System.Windows.Shapes.Rectangle();
            myRect.Stroke = System.Windows.Media.Brushes.Black;
            if (map1.map[y, x].Surface == Tile.Tile_type.grass)
            {
                myRect.Fill = System.Windows.Media.Brushes.LightGreen;
            }
            else if (map1.map[y, x].Surface == Tile.Tile_type.wall)
            {
                myRect.Fill = System.Windows.Media.Brushes.Gray;
            }
            else if (map1.map[y, x].Surface == Tile.Tile_type.water)
            {
                myRect.Fill = System.Windows.Media.Brushes.Blue;
            }
            else if (map1.map[y, x].Surface == Tile.Tile_type.lava)
            {
                myRect.Fill = System.Windows.Media.Brushes.OrangeRed;
            }
            else if (map1.map[y, x].Surface == Tile.Tile_type.bush)
            {
                myRect.Fill = System.Windows.Media.Brushes.DarkGreen;
            }
            myRect.Height = 50;
            myRect.Width = 50;
            m.canvas1.Children.Add(myRect);
            Canvas.SetTop(myRect, y*50);
            Canvas.SetLeft(myRect, x*50);
            Canvas.SetZIndex(myRect, 0);
            if (map1.map[y, x].Surface == Tile.Tile_type.bush)
            {
                Canvas.SetZIndex(myRect, 4);
            }
            if (map1.map[y, x].Loot != null)
            {
                map1.map[y, x].Icon = map1.map[y, x].Loot.get_view();
                m.canvas1.Children.Add(map1.map[y, x].Icon);
                Canvas.SetTop(map1.map[y, x].Icon, y*50 + 5);
                Canvas.SetLeft(map1.map[y, x].Icon, x*50 + 5);
                Canvas.SetZIndex(map1.map[y, x].Icon, 2);
            }
        }
        public override void SetScrollBar(ScoreMediator m)
        {
            canvas_scrollbar.ScrollToVerticalOffset(current.Y * 50 + 5 - canvas_scrollbar.ActualHeight / 2);
            canvas_scrollbar.ScrollToHorizontalOffset(current.X * 50 + 5 - canvas_scrollbar.ActualWidth / 2);
        }
    }
}
