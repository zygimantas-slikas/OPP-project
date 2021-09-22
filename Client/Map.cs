using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Map
    {
        public enum Room_satate { empty, waiting, full };
        public int max_players { get; }
        public int current_players { get; set; }
        public int map_size { get; }
        public Room_satate state { get; }
        public Tile[,] map;
        public Map()
        {

        }
        public void From_json(string json)
        {

        }

    }
}
