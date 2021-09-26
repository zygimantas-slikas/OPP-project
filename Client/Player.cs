using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; }
        public string Name { get; }
        public List<Item> Inventory { get; set; }
        public Player(string name, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Name = name;
            Inventory = new List<Item>();
            this.Health = 100;
        }
        public Player(string name)
        {
            this.X = 0;
            this.Y = 0;
            this.Name = name;
            Inventory = new List<Item>();
            this.Health = 100;
        }
    }

}
