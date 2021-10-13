using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    abstract public class Item
    {
        public string take(Player p, Tile[,] map)
        {
            return "json";
        }
        public string use(Player p, Tile[,] map)
        {
            return "json";
        }
        abstract public Item Clone();
    }
    public class Berry : Item
    {
        public virtual int Points { get; protected set; }
        public virtual int Heal { get; protected set; }
        public override Berry Clone()
        {
            Berry c = new Berry();
            c.Points = this.Points;
            c.Heal = this.Heal;
            return c;
        }
    }
    class BlueBerry : Berry
    {
        public BlueBerry()
        {
            this.Points = 200;
            this.Heal = 20;
        }
    }
    class RedBerry : Berry
    {
        public RedBerry()
        {
            this.Points = 100;
            this.Heal = 10;
        }
    }
    public class Gun : Item
    {
        public virtual int Damage { get; protected set; }
        public virtual int Ammo { get; protected set; }
        public override Item Clone()
        {
            Gun c = new Gun();
            c.Damage = this.Damage;
            c.Ammo = this.Ammo;
            return c;
        }
    }
    class BlueGun : Gun
    {
        public BlueGun()
        {
            this.Damage = 10;
            this.Ammo = 12;
        }
    }
    class RedGun : Gun
    {
        public RedGun()
        {
            this.Damage = 30;
            this.Ammo = 6;
        }
    }
}
