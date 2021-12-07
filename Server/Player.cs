using System.Collections.Generic;

namespace Server
{
    public class Player
    {
        public int Color { get; protected set; }
        public int X { get; set; }
        public int Y { get; set; } 
        public /*virtual*/ string Con_id { get; set; }
        public virtual int Health { get; protected set; }
        public int Points { get; protected set; }
        public string Name { get; }
        public List<Item> Inventory { get; set; }
        public virtual string Comment { get; set; }
        public Player(int color, string id, string name, int x, int y, List<Item> inventory)
        {
            this.Color = color;
            this.X = x;
            this.Y = y;
            this.Name = name;
            this.Points = 0;
            this.Inventory = inventory;
            this.Con_id = id;
            this.Health = 100;
            this.Comment = "Connected!";
        }
        public Player(string id, string name)
        {
            this.X = 0;
            this.Y = 0;
            this.Name = name;
            this.Points = 0;
            this.Con_id = id;
            this.Inventory = new List<Item>();
            this.Health = 100;
        }
        public Player() { }
        public void addItem(Item item)
        {
            this.Inventory.Add(item);
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
    }

}
