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
    public class Map
    {
        public enum Room_satate { empty, waiting, full };
        public int max_players { get; set; }
        public int current_players { get; set; }
        public int map_size { get; set; }
        public Room_satate state { get; set; }
        public int level { get; protected set; }
        public Tile[,] map;
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
            level = (int)pj["level"];
            map = new Tile[map_size, map_size];
            for(int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    Tile t = new Tile();
                    t.Surface = (Tile.Tile_type)(int)pj["map"][i][j]["Surface"];
                    Item item1 = null;
                    var a = pj["map"][i][j]["Loot"].Type.ToString();
                    if (pj["map"][i][j]["Loot"].Type.ToString() != "Null")
                    {
                        switch ((string)pj["map"][i][j]["Loot"]["Type"])
                        {
                            case "BlueBerry":
                                item1 = new BlueBerry();
                                break;
                            case "RedBerry":
                                item1 = new RedBerry();
                                break;
                            case "BlueGun":
                                item1 = new BlueGun();
                                break;
                            case "RedGun":
                                item1 = new RedGun();
                                break;
                            case "BlueMedicKit":
                                item1 = new BlueMedicKit();
                                break;
                            case "RedMedicKit":
                                item1 = new RedMedicKit();
                                break;
                            default:
                                item1 = null;
                                break;
                        }
                    }
                    t.Loot = item1;
                    t.Player_Standing = (String)pj["map"][i][j]["Player_Standing"];
                    map[i, j] = t;
                }
            }
        }
        public void Json_update (string json)
        {

        }
    }
}
