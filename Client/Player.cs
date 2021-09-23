using System.Collections.Generic;

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
    }

}
