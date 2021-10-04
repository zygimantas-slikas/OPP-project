using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Item
    {
        public string take(Player p, Tile[,] map)
        {
            return "json";
        }
        public string use(Player p, Tile[,] map)
        {
            return "json";
        }

    }
    public class RedBerry : Item
    {

    }
    public class BlueBerry : Item
    {

    }
    public class Dynamite: Item
    {

    }
    public class Gun: Item
    {

    }
}
