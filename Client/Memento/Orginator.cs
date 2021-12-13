using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Memento
{
    class Orginator
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

        public Memento SaveMemento()
        {
            return new Memento(x, y, points);
        }

        public void RestoreMemento(Memento memento)
        {
            X = memento.X;
            Y = memento.Y;
            Points = memento.Points;
        }
    }
}
