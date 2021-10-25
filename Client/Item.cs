using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Client
{
    abstract public class Item
    {
        abstract public Rectangle get_view();
        public string Type { get; protected set; }
        public Item() { }
        public Item(string type)
        {
            this.Type = type;
        }
        public string take(Player p, Tile[,] map)
        {
            return "json";
        }
        public string use(Player p, Tile[,] map)
        {
            return "json";
        }
        abstract public void PickupEffect(Player p);
        abstract public int GetNumber();
    }
    abstract public class Berry : Item
    {
        public virtual int Points { get; protected set; }
        public override void PickupEffect(Player p)
        {
            p.Addpoints(this.Points);
        }

        public override int GetNumber()
        {
            throw new NotImplementedException();
        }
    }
    class BlueBerry : Berry
    {
        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\blueberry.jpg", UriKind.RelativeOrAbsolute));
            img.Fill = myBrush;
            return img;
        }
        public BlueBerry()
        {
            this.Points = 200;
            this.Type = this.GetType().Name;
        }

    }
    class RedBerry : Berry
    {
        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\redberry.jpg", UriKind.RelativeOrAbsolute));
            img.Fill = myBrush;
            return img;
        }
        public RedBerry()
        {
            this.Points = 100;
            this.Type = this.GetType().Name;
        }
    }
    abstract class MedicKit : Item
    {
        public virtual int Heal { get; protected set; }
        public override void PickupEffect(Player p)
        {
            p.AddHealth(this.Heal);
        }
        public override int GetNumber()
        {
            throw new NotImplementedException();
        }
    }
    class BlueMedicKit : MedicKit
    {
        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\bluemedic.png", UriKind.RelativeOrAbsolute));
            img.Fill = myBrush;
            return img;
        }
        public BlueMedicKit()
        {
            this.Heal = 20;
            this.Type = this.GetType().Name;
        }
    }
    class RedMedicKit : MedicKit
    {
        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\redmedic.png", UriKind.RelativeOrAbsolute));
            img.Fill = myBrush;
            return img;
        }
        public RedMedicKit()
        {
            this.Heal = 10;
            this.Type = this.GetType().Name;
        }
    }
    abstract public class Gun : Item
    {
        public virtual int Damage { get; protected set; }
        public virtual int Ammo { get; protected set; }
        public override void PickupEffect(Player p)
        {

        }

        public override int GetNumber()
        {
            throw new NotImplementedException();
        }
    }
        class BlueGun : Gun
        {
        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\bluegun.png", UriKind.RelativeOrAbsolute));
            img.Fill = myBrush;
            return img;
        }
        public BlueGun()
        {
            this.Damage = 10;
            this.Ammo = 12;
            this.Type = this.GetType().Name;
        }
    }
    class RedGun : Gun
    {
        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\redgun.png", UriKind.RelativeOrAbsolute));
            img.Fill = myBrush;
            return img;
        }
        public RedGun()
        {
            this.Damage = 30;
            this.Ammo = 6;
            this.Type = this.GetType().Name;
        }
    }

    abstract public class Trap : Item
    {
        public virtual int Damage { get; protected set; }
        public override void PickupEffect(Player p)
        {
            p.AddDamage(this.Damage);
        }

        public override int GetNumber()
        {
            throw new NotImplementedException();
        }
    }
    class VisibleTrap : Trap
    {
        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\trap.png", UriKind.RelativeOrAbsolute));
            img.Fill = myBrush;
            return img;
        }

        public VisibleTrap()
        {
            this.Damage = 30;
            this.Type = this.GetType().Name;
        }
    }
    class InVisibleTrap : Trap
    {
        public override Rectangle get_view()
        {
            Rectangle img = new Rectangle();
            img.Width = 40;
            img.Height = 40;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\invisibletrap.png", UriKind.RelativeOrAbsolute));
            img.Fill = myBrush;
            return img;
        }

        public InVisibleTrap()
        {
            this.Damage = 35;
            this.Type = this.GetType().Name;
        }
    }

    public class Fire : Item
    {
        public virtual int TimesStepped { get; protected set; }
        public Fire(int timesStepped)
        {
            this.TimesStepped = timesStepped;
            this.Type = this.GetType().Name;
        }

        public override Rectangle get_view()
        {
            throw new NotImplementedException();
        }

        public override void PickupEffect(Player p)
        {
            this.TimesStepped++;
        }

        public override int GetNumber()
        {
            return TimesStepped;
        }
    }
}
