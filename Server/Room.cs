﻿using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


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
            Player p = new Player(con_id, name);

            if (this.map[0, 0].Player_Standing == null) {
                this.map[0, 0].Player_Standing = name;
                p.X = 0;
                p.Y = 0;
            }
            else if (this.map[0, map_size - 1].Player_Standing == null) {
                this.map[0, map_size - 1].Player_Standing = name;
                p.X = 0;
                p.Y = map_size - 1;
            }
            else if (this.map[map_size - 1, 0].Player_Standing == null) {
                this.map[map_size - 1, 0].Player_Standing = name;
                p.X = map_size - 1;
                p.Y = 0;
            }
            else if (this.map[map_size - 1, map_size - 1].Player_Standing == null) {
                this.map[map_size - 1, map_size - 1].Player_Standing = name;
                p.X = map_size - 1;
                p.Y = map_size - 1;
            }
            players.Add(p);
            this.current_players++;
        }
        public void Remove_player(string con_id)
        {
            Player p = players.Find(x => x.Con_id == con_id);
            map[p.X, p.Y].Player_Standing = null;
            this.players.Remove(p);
            this.current_players--;
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
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, serializerSettings);

            return json;//"[" + JsonSerializer.Serialize(map_size) + "," + JsonSerializer.Serialize(tiles) + "]";
        }

        public string Players_to_Json()
        {
            string t1 = System.Text.Json.JsonSerializer.Serialize(players);
            Regex re = new Regex("\"Con_id\":\"[^\"]+\",");
            MatchCollection matches = re.Matches(t1);
            for(int i = matches.Count-1; i >= 0; i--)
            {
                t1 = t1.Remove(matches[i].Index, matches[i].Length);
            }
            return t1;
        }


    }

}
