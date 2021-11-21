using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Client.Proxy
{
    public abstract class IMapControl
    {
        public ScrollViewer canvas_scrollbar { get; set; }
        public Player current { get; set; }
        public IMapControl(ScrollViewer sc, Player p)
        {
            this.canvas_scrollbar = sc;
            this.current = p;
        }
        public abstract void DrawMap(Map map1, Dictionary<string, Shape> players_gui, Canvas canvas1);
        public abstract void DrawTile(Map map1, Dictionary<string, Shape> players_gui, Canvas canvas1, int x, int y);
        public abstract void SetScrollBar();

    }
}
