using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Player
    {
        public int x, y; //coordinates
        public string Con_id { get; }
        public int Health { get; }
        public List<Item> Inventory { get; set; }
        public Player()
        {
            Inventory = new List<Item>();
            this.Health = 100;
        }
    }

}
