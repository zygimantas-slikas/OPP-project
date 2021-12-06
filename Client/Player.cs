using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Client.Strategy;
using Client.Observer;
using System.Windows.Shapes;
using System.Windows.Controls;

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
        public int currentItem { get; private set; }
        private IMovementStrategy strategy;
        [JsonConstructor]
        public Player(int Health, string Name, int X, int Y, int points, List<Item> Inventory)
        {
            this.X = X;
            this.Y = Y;
            this.Name = Name;
            this.Points = points;
            this.Inventory = Inventory;
            this.Health = Health;
            this.currentItem = 0;
        }
        public Player(string name)
        {
            this.currentItem = 0;
            this.X = 0;
            this.Y = 0;
            this.Name = name;
            this.Inventory = new List<Item>();
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

        public void addItem(Item item)
        {
            this.Inventory.Add(item);
            //if(this.Inventory.Count() == 1)
            //{
            //    this.currentItem = item;
            //}
        }

        public void switchItem()
        {
            this.currentItem = (++this.currentItem) % this.Inventory.Count;
            //this.currentItem = this.Inventory.Find(this.currentItem).Next != null ? this.Inventory.Find(this.currentItem).Next.Value : this.Inventory.First.Value;
        }
        public void unSwitchItem()
        {
            this.currentItem = (--this.currentItem + this.Inventory.Count) % this.Inventory.Count;
            //this.currentItem = this.Inventory.Find(this.currentItem).Next != null ? this.Inventory.Find(this.currentItem).Next.Value : this.Inventory.Last.Value;
        }

        private List<IScore> scoreObservers = new List<IScore>();
        public void Attach(IScore observer)
        {
            scoreObservers.Add(observer);
        }
        public void Detach(IScore observer)
        {
            scoreObservers.Remove(observer);
        }
        public void Notify(IEnumerable<string> names, IEnumerable<string> created, List<Player> players1, Dictionary<String, Shape> players_gui, Canvas canvas1)
        {
            foreach (IScore observer in scoreObservers)
            {
                observer.Update(names, created, players1, players_gui, canvas1);
            }
        }
        public int getObserverCount()
        {
            return scoreObservers.Count;
        }
        public void Addpoints(int pts)
        {
            this.Points += pts;
        }

        public void AddDamage(int hp)
        {
            this.Health -= hp;
        }

        public void AddHealth(int hp)
        {
            this.Health += hp;
        }
    }
}
