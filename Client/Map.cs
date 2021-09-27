using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Map
    {
        public enum Room_satate { empty, waiting, full };
        public int max_players { get; set; }
        public int current_players { get; set; }
        public int map_size { get; set; }
        public Room_satate state { get; set; }
        public Tile[,] map;
        //public List<Player> players;
        public Map()
        {

        }
        public void From_json(string json)
        {
            JObject pj = JObject.Parse(json);
            max_players = (int)pj["max_players"];
            current_players = (int)pj["current_players"];
            map_size = (int)pj["map_size"];
            state = (Room_satate)(int)pj["state"];
            map = new Tile[map_size, map_size];
            for(int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    Tile t = new Tile();
                    t.Surface = (Tile.Tile_type)(int)pj["map"][i][j]["Surface"];
                    t.Loot = null; //pj["map"][i][j]["Loot"];
                    t.Player_Standing = (String)pj["map"][i][j]["Player_Standing"];
                    map[i, j] = t;
                }
            }

            //var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            //var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            //this.map1 = JsonConvert.DeserializeObject<Map>(json_text, serializerSettings);
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
