using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Text.Json;

namespace Server
{
    public class Room
    {
        public enum Room_satate { empty, waiting, full };
        public int Id { get; }
        public int max_players { get; }
        public int current_players { get; set; }
        public int map_size { get; }
        public Room_satate state { get; }

        public List<Player> players;
        public Tile[,] map;
        public Room(int id, int players_count, int map_size)
        {
            this.map_size = map_size;
            var t = Task.Run(() => this.Generate_map());
            this.Id = id;
            this.max_players = players_count;
            this.current_players = 0;
            this.players = new List<Player>(players_count);
            this.map = new Tile[map_size, map_size];
            this.state = Room_satate.empty;
            t.Wait();
        }
        public void Add_player(string con_id, string name)
        {
            Player p = new Player(con_id);

            if (this.map[0, 0].Player_Standing == false) {
                this.map[0, 0].Player_Standing = true;
                p.X = 0;
                p.Y = 0;
            }
            else if (this.map[0, map_size - 1].Player_Standing == false) {
                this.map[0, map_size - 1].Player_Standing = true;
                p.X = 0;
                p.Y = map_size - 1;
            }
            else if (this.map[map_size - 1, 0].Player_Standing == false) {
                this.map[map_size - 1, 0].Player_Standing = true;
                p.X = map_size - 1;
                p.Y = 0;
            }
            else if (this.map[map_size - 1, map_size - 1].Player_Standing == false) {
                this.map[map_size - 1, map_size - 1].Player_Standing = true;
                p.X = map_size - 1;
                p.Y = map_size - 1;
            }
            players.Add(p);
            this.current_players++;
        }

        public void Add_Tile(int x, int y, Tile.Tile_type tile_Type)
        {
            map[x, y] = new Tile(tile_Type);
        }

        private void Generate_map()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (j == 2 && i == 2)
                        map[i, j] = new Tile(Tile.Tile_type.wall);
                    else
                        map[i, j] = new Tile();
        }

        public string To_Json()
        {
            List<Tile> tiles = new List<Tile>();
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    tiles.Add(map[i, j]);

            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, serializerSettings);
            return json;//"[" + JsonSerializer.Serialize(map_size) + "," + JsonSerializer.Serialize(tiles) + "]";
        }
        public void From_Json(string json)
        {

        }
        //move function
        public string Players_to_Json()
        {
            string t1 = System.Text.Json.JsonSerializer.Serialize(players);
            //remove conId ("conID":"[\d\D]+",)
            return t1;
        }


    }

}
