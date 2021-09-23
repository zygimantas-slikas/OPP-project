using System.Collections.Generic;

namespace Server
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; } 
        public string Con_id { get; }
        public int Health { get; }
        public string Name { get; }
        public List<Item> Inventory { get; set; }
        public Player(string id, string name, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Name = name;
            Inventory = new List<Item>();
            this.Con_id = id;
            this.Health = 100;
        }
    }

}
