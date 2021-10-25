﻿using System;
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
        public LinkedList<Item> Inventory { get; set; }
        public Item currentItem { get; private set; }
        private IMovementStrategy strategy;
        [JsonConstructor]
        public Player(int Health, string Name, int X, int Y, LinkedList<Item> Inventory)
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
            Inventory = new LinkedList<Item>();
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
            this.Inventory.AddLast(item);
            if(this.Inventory.Count() == 1)
            {
                this.currentItem = item;
            }
        }

        public void switchItem()
        {
            this.currentItem = this.Inventory.Find(this.currentItem).Next != null ? this.Inventory.Find(this.currentItem).Next.Value : this.Inventory.First.Value;
        }
        public void unSwitchItem()
        {
            this.currentItem = this.Inventory.Find(this.currentItem).Next != null ? this.Inventory.Find(this.currentItem).Next.Value : this.Inventory.Last.Value;
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
