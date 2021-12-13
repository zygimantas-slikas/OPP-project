using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Memento
{
    public class Memento
    {
        int x; 
        int y; 
        int points;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public Memento(int x, int y, int points)
        {
            this.x = x;
            this.y = y;
            this.points = points;
        }
    }
}
