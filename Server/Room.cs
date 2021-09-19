using System.Collections.Generic;
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

        private List<Player> players;
        private Tile[,] map;
        public Room(int id, int players_count, int map_size)
        {
            this.Id = id;
            this.max_players = players_count;
            this.map_size = map_size;
            this.Generate_map();
            this.current_players = 0;
            this.players = new List<Player>(players_count);
            this.map = new Tile[map_size, map_size];
            this.state = Room_satate.empty;
        }
        public void Add_player(string con_id)
        {
            players.Add(new Player(con_id));

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

    }

}
