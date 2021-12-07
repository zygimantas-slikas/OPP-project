using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System;
using Server.Builder;

namespace Server
{
    public class Room
    {
        public enum Room_satate { empty, waiting, full };
        public int Id { get; }
        public int max_players { get; }
        public int current_players { get; protected set; }
        public int map_size { get; }
        public Room_satate state { get; protected set; }
        public int level { get; }
        public List<Player> players;
        public Tile[,] map;
        public Room(int id, int players_count, int map_size, int level, int type)
        {
            MapBuilder builder;
            if (type == 1) builder = new MapBuilder1(map_size, level); 
            else builder = new MapBuilder2(map_size, level);
            var t = Task.Run(() => this.Construct(builder));
            this.level = level;
            this.map_size = map_size;
            this.Id = id;
            this.max_players = players_count;
            this.current_players = 0;
            this.players = new List<Player>(players_count);
            this.state = Room_satate.empty;
            t.Wait();
            map = builder.Get_result();
        }
        public void Construct(MapBuilder build)
        {
            //build.add_lakes();
            //build.add_bushes();
            //build.add_lava();
            //build.add_walls();
            //build.fill_grass();
            build.TemplateAdd();
        }
        public int Check_players_helath()
        {
            int found = 0;
            foreach(Player player_1 in this.players)
            {
                if (player_1.Health <= 0)
                {
                    found++;
                }
            }
            return found;
        }
        public void Add_player(Player p)
        {
            if (this.map[0, 0].Player_Standing == "")
            {
                this.map[0, 0].Player_Standing = p.Name;
                p.X = 0;
                p.Y = 0;
            }
            else if (this.map[0, map_size - 1].Player_Standing == "")
            {
                this.map[0, map_size - 1].Player_Standing = p.Name;
                p.X = 0;
                p.Y = map_size - 1;
            }
            else if (this.map[map_size - 1, 0].Player_Standing == "")
            {
                this.map[map_size - 1, 0].Player_Standing = p.Name;
                p.X = map_size - 1;
                p.Y = 0;
            }
            else if (this.map[map_size - 1, map_size - 1].Player_Standing == "")
            {
                this.map[map_size - 1, map_size - 1].Player_Standing = p.Name;
                p.X = map_size - 1;
                p.Y = map_size - 1;
            }
            players.Add(p);
            this.current_players++;
            state = Room_satate.waiting;
            if (current_players == max_players)
            {
                state = Room_satate.full;
            }
        }
        public void Add_player(string con_id, string name)
        {
            List<Item> inv = new List<Item>();
            Player p = new Player(players.Count, con_id, name, 0, 0, inv);
            if (this.map[0, 0].Player_Standing == "") {
                this.map[0, 0].Player_Standing = name;
                p.X = 0;
                p.Y = 0;
            }
            else if (this.map[0, map_size - 1].Player_Standing == "") {
                this.map[0, map_size - 1].Player_Standing = name;
                p.X = 0;
                p.Y = map_size - 1;
            }
            else if (this.map[map_size - 1, 0].Player_Standing == "") {
                this.map[map_size - 1, 0].Player_Standing = name;
                p.X = map_size - 1;
                p.Y = 0;
            }
            else if (this.map[map_size - 1, map_size - 1].Player_Standing == "") {
                this.map[map_size - 1, map_size - 1].Player_Standing = name;
                p.X = map_size - 1;
                p.Y = map_size - 1;
            }
            players.Add(p);
            this.current_players++;
            state = Room_satate.waiting;
            if (current_players == max_players)
            {
                state = Room_satate.full;
            }
        }
        public void Remove_player(string con_id)
        {
            Player p = players.Find(x => x.Con_id == con_id);
            map[p.X, p.Y].Player_Standing = null;
            this.players.Remove(p);
            this.current_players--;
        }
        public string To_Json()
        {
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, serializerSettings);
            Regex re = new Regex("\"Con_id\": \"[^\"]+\",");
            MatchCollection matches = re.Matches(json);
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                json = json.Remove(matches[i].Index, matches[i].Length);
            }
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
