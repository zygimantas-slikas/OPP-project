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
        public int Health { get; set; }
        public int Points { get; set; }
        public string Name { get; protected set; }
        public List<Item> Inventory { get; set; }
        private IMovementStrategy strategy;
        [JsonConstructor]
        public Player(int Health, string Name, int X, int Y, List<Item> Inventory)
        {
            this.X = X;
            this.Y = Y;
            this.Name = Name;
            this.Inventory = Inventory;
            this.Health = Health;
        }
        public Player(string name)
        {
            this.X = 0;
            this.Y = 0;
            this.Name = name;
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
