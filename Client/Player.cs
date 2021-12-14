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
using Client.Iterator;
using Client.Mediator;

namespace Client
{
    public class Player : IAggregate<IScore>
    {
        public int Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; set; }
        public int Points { get; set; }
        public string Name { get; protected set; }
        public List<Item> Inventory { get; set; }
        public int currentItem { get; private set; }
        private IMovementStrategy strategy;
        public string Comment { get; set; }
        protected ScoreMediator m;
        public IScore this[int itemIndex]
        {
            get => scoreObservers[itemIndex];
            set => scoreObservers.Add((IScore)value);
        }

        [JsonConstructor]
        public Player(int color, int Health, string Name, int X, int Y, int points, List<Item> Inventory, string comment, ScoreMediator mediator)
        {
            this.Color = color;
            this.X = X;
            this.Y = Y;
            this.Name = Name;
            this.Points = points;
            this.Inventory = Inventory;
            this.Health = Health;
            this.currentItem = 0;
            this.Comment = comment;
            this.m = mediator;
        }
        public Player(/*int Health, string Name, int X, int Y, int points, List<Item> Inventory, string comment*/)
        {
            //this.X = X;
            //this.Y = Y;
            //this.Name = Name;
            //this.Points = points;
            //this.Inventory = Inventory;
            //this.Health = Health;
            //this.currentItem = 0;
            //this.Comment = comment;
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
        public void Notify(IEnumerable<string> names, IEnumerable<string> created, List<Player> players1, Dictionary<String, Shape> players_gui, Canvas canvas1, Player score)
        {
            var iterator = score.GetIterator();
            for (var o = iterator.First(); iterator.HasNext(); o = iterator.Next())
            {
                o.Update(names, created, players1, players_gui, canvas1);
            }
        }
        public int getObserverCount()
        {
            return scoreObservers.Count;
        }

        public void SendMessage(string msg)
        {
            m.Notify(this, msg);
        }

        public void ReceiveMessage(string msg)
        {

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
        public void AddComment(string c)
        {
            this.Comment = c;
        }

        public IIterator<IScore> GetIterator()
        {
            return new ConcreteIterator<IScore>(this);
        }

        public int Count()
        {
            return scoreObservers.Count;
        }
    }
}
