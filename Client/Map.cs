using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client
{
    class Map
    {
        public enum Room_satate { empty, waiting, full };
        public int max_players { get; }
        public int current_players { get; set; }
        public int map_size { get; set; }
        public Room_satate state { get; }
        public Tile[,] map;
        public List<Player> players;
        public Map()
        {

        }
        public void From_json(string json)
        {
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

            //string json = JsonConvert.SerializeObject(people, Formatting.Indented, serializerSettings);
            //JsonConvert.DeserializeObject<Map>(json, serializerSettings);
            //this.map = new Tile[map_size, map_size];
            //for (int i = 0; i < map_size; i++)
            //{
            //    for (int j = 0; j < map_size; j++)
            //    {
            //        this.map[i, j] = new Tile(tiles[i * j].Surface);
            //        this.map[i, j].Player_Standing = tiles[i * j].Player_Standing;
            //    }
            //}
        }

    }
}
