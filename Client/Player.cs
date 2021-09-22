using System.Collections.Generic;

namespace Client
{
    public class Player
    {
        public int x, y; //coordinates
        public int Health { get; }
        public List<Item> Inventory { get; set; }
        public Player()
        {
            Inventory = new List<Item>();
            this.Health = 100;
        }
    }

}
