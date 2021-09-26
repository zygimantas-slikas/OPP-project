using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System;

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

        private List<Player> players;
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
            var rand = new Random();
            int x = rand.Next(0, map_size), y = rand.Next(0, map_size);
            //check if no one else has same coordinates
            players.Add(new Player(con_id, name, x, y));
            this.current_players++;
        }
        private void Generate_map()
        {
            
        }
        public string To_Json()
        {
            // test
            string test = this.map_size.ToString();
            return test;
        }
        public void From_Json(string json)
        {

        }
        //move function
        public string Players_to_Json()
        {
            string t1 = JsonSerializer.Serialize(players);
            //remove conId ("conID":"[\d\D]+",)
            return t1;
        }


    }

}
