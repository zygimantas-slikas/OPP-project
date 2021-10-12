using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Client.Strategy;

namespace Client
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Con_id { get; }
        public int Health { get; }
        public string Name { get; }
        public List<Item> Inventory { get; set; }
        private IMovementStrategy strategy;

        [JsonConstructor]
        public Player(string Con_id, int Health, string Name, int X, int Y, List<Item> Inventory)
        {
            this.X = X;
            this.Y = Y;
            this.Name = Name;
            this.Inventory = Inventory;
            this.Con_id = Con_id;
            this.Health = Health;
        }
        public Player(string id, string name)
        {
            this.X = 0;
            this.Y = 0;
            this.Name = name;
            this.Con_id = id;
            Inventory = new List<Item>();
            this.Health = 100;
        }

        public void setStrategy(IMovementStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void move()
        {
            this.strategy.Move();
        }
    }

}
