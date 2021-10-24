using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Rooms_list_view_obj
    {
        public string map_id { get; set; }
        public string players { get; set; }
        public string map_size { get; set; }
        public Rooms_list_view_obj(string map_id, string players, string map_size)
        {
            this.map_id = map_id;
            this.map_size = map_size;
            this.players = players;
        }
    }
}
