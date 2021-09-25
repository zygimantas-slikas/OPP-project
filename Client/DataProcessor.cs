using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Client
{
    class DataProcessor
    {
        public DataProcessor()
        {

        }

        public string SerializeJson(Player player)
        {
            return JsonSerializer.Serialize(player);
        }

        public string DeserializeJson(string json)
        {
            return ""; //JsonSerializer.Deserialize(json, Player);
        }
    }
}
