using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    abstract public class Item
    {
        public string Type { get; protected set; }
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
        public override Berry Clone()
        {
            Berry c = new Berry();
            c.Type = this.Type;
            c.Points = this.Points;
            return c;
        }
    }
    class BlueBerry : Berry
    {
        public BlueBerry()
        {
            this.Points = 200;
            this.Type = this.GetType().Name;
        }
    }
    class RedBerry : Berry
    {
        public RedBerry()
        {
            this.Points = 100;
            this.Type = this.GetType().Name;
        }
    }
    class MedicKit : Item
    {
        public virtual int Heal { get; protected set; }
        public override MedicKit Clone()
        {
            MedicKit c = new MedicKit();
            c.Type = this.Type;
            c.Heal = this.Heal;
            return c;
        }
    }
    class BlueMedicKit : MedicKit
    {
        public BlueMedicKit()
        {
            this.Heal = 20;
            this.Type = this.GetType().Name;
        }
    }
    class RedMedicKit : MedicKit
    {
        public RedMedicKit()
        {
            this.Heal = 10;
            this.Type = this.GetType().Name;
        }
    }
    public class Gun : Item
    {
        public virtual int Damage { get; protected set; }
        public virtual int Ammo { get; protected set; }
        public override Item Clone()
        {
            Gun c = new Gun();
            c.Type = this.Type;
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
            this.Type = this.GetType().Name;
        }
    }
    class RedGun : Gun
    {
        public RedGun()
        {
            this.Damage = 30;
            this.Ammo = 6;
            this.Type = this.GetType().Name;
        }
    }
    public class Trap : Item
    {
        public virtual int Damage { get; protected set; }
        public override Item Clone()
        {
            Trap c = new Trap();
            c.Type = this.Type;
            c.Damage = this.Damage;
            return c;
        }
    }
    class VisibleTrap : Trap
    {
        public VisibleTrap()
        {
            this.Damage = 30;
            this.Type = this.GetType().Name;
        }
    }
    class InVisibleTrap : Trap
    {
        public InVisibleTrap()
        {
            this.Damage = 35;
            this.Type = this.GetType().Name;
        }
    }
}
