using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Client.Proxy
{

    public class MapDrawProxy : IMapControl
    {
        protected bool[,] bitmap;
        protected MapDrawer drawer;
        protected Map map2;
        Dictionary<string, Shape> players_gui2;
        Canvas canvas2;
        public MapDrawProxy(ScrollViewer sc, Player p) : base(sc, p)
        {
            drawer = new MapDrawer(sc, p);
        }
        public override void DrawMap(Map map1, Dictionary<string, Shape> players_gui, Canvas canvas1)
        {
            this.map2 = map1;
            canvas2 = canvas1;
            players_gui2 = players_gui;
            drawer.canvas_scrollbar = this.canvas_scrollbar;
            drawer.current = this.current;
            canvas1.Children.Clear();
            canvas1.Height = Convert.ToInt32(map1.map_size) * 50 + 20;
            canvas1.Width = Convert.ToInt32(map1.map_size) * 50 + 20;
            drawer.SetScrollBar();
            this.bitmap = new bool[map1.map_size, map1.map_size];
            int x1, x2, y1, y2;
            x1 = current.X;
            y1 = current.Y;
            x2 = x1 + ((int)canvas_scrollbar.ActualWidth) / 50;
            y2 = y1 + ((int)canvas_scrollbar.ActualHeight) / 50;
            if (y2 >= map1.map_size)
            {
                y1 -= y2 - (map1.map_size - 1);
                y2 -= y2 - (map1.map_size - 1);
            }
            if (x2 >= map1.map_size)
            {
                x1 -= x2 - (map1.map_size - 1);
                x2 -= x2 - (map1.map_size - 1);
            }
            for (int i = y1; i <= y2; i++)
            {
                for (int j = x1; j <= x2; j++)
                {
                    this.bitmap[j, i] = true;
                    this.drawer.DrawTile(map1, players_gui, canvas1, j, i);
                }
            }
            foreach (var el in players_gui.Values)
            {
                canvas1.Children.Remove(el);
                canvas1.Children.Add(el);
                Canvas.SetZIndex(el, 3);
            }
        }

        public override void DrawTile(Map map1, Dictionary<string, Shape> players_gui, Canvas canvas1, int x, int y)
        {
            
        }

        public override void SetScrollBar()
        {
            drawer.canvas_scrollbar = this.canvas_scrollbar;
            drawer.current = this.current;
            drawer.SetScrollBar();
            int x1, x2, y1, y2;
            x1 = ((int)canvas_scrollbar.HorizontalOffset) / 50;
            y1 = ((int)canvas_scrollbar.VerticalOffset) / 50;
            x2 = x1 + ((int)canvas_scrollbar.ActualWidth) / 50 +1;
            y2 = y1 + ((int)canvas_scrollbar.ActualHeight) / 50 +1;
            if (y2 >= map2.map_size)
            {
                y1 -= y2 - (map2.map_size - 1);
                y2 -= y2 - (map2.map_size - 1);
            }
            if (x2 >= map2.map_size)
            {
                x1 -= x2 - (map2.map_size - 1);
                x2 -= x2 - (map2.map_size - 1);
            }
            x1 -= 1;
            y1 -= 1;
            if (x1 < 0)
            {
                x1 = 0;
            }
            if (y1 < 0)
            {
                y1 = 0;
            }
            for (int i = y1; i <= y2; i++)
            {
                for (int j = x1; j <= x2; j++)
                {
                    if (this.bitmap[j,i] == false)
                    {
                        this.bitmap[j, i] = true;
                        this.drawer.DrawTile(map2, players_gui2, canvas2, j, i);
                    }
                }
            }
        }
    }
}
