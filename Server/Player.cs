using System.Collections.Generic;

namespace Server
{
    public class Player
    {
        public int x, y; //coordinates
        public string Con_id { get; }
        public int Health { get; }
        public List<Item> Inventory { get; set; }
        public Player(string id)
        {
            Inventory = new List<Item>();
            this.Con_id = id;
            this.Health = 100;
        }
    }

}
