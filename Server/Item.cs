using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Server
{
    public abstract class Item : Cloneable
    {
        public string Type { get; protected set; }
        public enum Crate_type { general, guns, health };
        public virtual Crate_type Storage { get; set; }
        public string take(Player p, Tile[,] map)
        {
            return "json";
        }
        public string use(Player p, Tile[,] map)
        {
            return "json";
        }
        public abstract Cloneable Clone();
        public abstract void PickupEffect(Player p);

        public virtual bool IsCrate() { return false; }

        public virtual void Add(Item component, Player p)
        {
            throw new NotImplementedException();
        }

        public virtual Item Remove(Item component, Player p, out bool crateOfItems)
        {
            throw new NotImplementedException();
        }
    }
    public class Berry : Item
    {
        public virtual int Points { get; protected set; }
        public override Cloneable Clone()
        {
            Berry c = new Berry();
            c.Type = new string(this.Type);
            c.Points = this.Points;
            return c;
        }

        public override void PickupEffect(Player p)
        {
            p.Addpoints(this.Points);
        }
    }
    public class BlueBerry : Berry
    {
        public BlueBerry()
        {
            this.Points = 200;
            this.Type = new string(this.GetType().Name);
        }
    }
    class RedBerry : Berry
    {
        public RedBerry()
        {
            this.Points = 100;
            this.Type = new string(this.GetType().Name);
        }
    }
    public class MedicKit : Item
    {
        public virtual int Heal { get; protected set; }
        public override Cloneable Clone()
        {
            MedicKit c = new MedicKit();
            c.Type = this.Type;
            c.Heal = this.Heal;
            return c;
        }

        public override void PickupEffect(Player p)
        {
            p.AddHealth(this.Heal);
        }
    }
    public class BlueMedicKit : MedicKit
    {
        public BlueMedicKit()
        {
            this.Heal = 20;
            this.Type = new string(this.GetType().Name);
        }
    }
    class RedMedicKit : MedicKit
    {
        public RedMedicKit()
        {
            this.Heal = 10;
            this.Type = new string(this.GetType().Name);
        }
    }
    public class Gun : Item
    {
        public virtual int Damage { get; protected set; }
        public virtual int Ammo { get; protected set; }
        public override Cloneable Clone()
        {
            Gun c = new Gun();
            c.Type = this.Type;
            c.Damage = this.Damage;
            c.Ammo = this.Ammo;
            return c;
        }

        public override void PickupEffect(Player p)
        {
            
        }
    }
    class BlueGun : Gun
    {
        public BlueGun()
        {
            this.Damage = 10;
            this.Ammo = 12;
            this.Type = new string(this.GetType().Name);
        }
    }
    class RedGun : Gun
    {
        public RedGun()
        {
            this.Damage = 30;
            this.Ammo = 6;
            this.Type = new string(this.GetType().Name);
        }
    }
    public class Trap : Item
    {
        public virtual int Damage { get; protected set; }
        public virtual bool Activated { get; protected set; }
        public override Cloneable Clone()
        {
            Trap c = new Trap();
            c.Type = this.Type;
            c.Damage = this.Damage;
            c.Activated = this.Activated;
            return c;
        }

        public override void PickupEffect(Player p)
        {
            //p.AddDamage(this.Damage);
            Activated = true;
        }
    }
    class VisibleTrap : Trap
    {
        public VisibleTrap()
        {
            this.Damage = 30;
            this.Type = this.GetType().Name;
            this.Activated = false;
        }
    }
    class InVisibleTrap : Trap
    {
        public InVisibleTrap()
        {
            this.Damage = 35;
            this.Type = this.GetType().Name;
            this.Activated = false;
        }
    }

    public class Fire : Item
    {
        public virtual int TimesStepped { get; protected set; }
        public Fire()
        {
            this.TimesStepped = 0;
            this.Type = this.GetType().Name;
        }

        public override Cloneable Clone()
        {
            Fire c = new Fire();
            c.Type = this.Type;
            return c;
        }

        public override void PickupEffect(Player p)
        {
            this.TimesStepped += 1;
        }
    }
}
