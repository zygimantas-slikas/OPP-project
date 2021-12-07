using Client.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.AbstractFactory
{
     public abstract class ItemsFactory
    {
        public abstract Berry Create_berry();
        public abstract Gun Create_gun();
        public abstract MedicKit Create_medic();
        public abstract Trap Create_trap();
        public abstract Crate Create_crate();
        public abstract Crate Create_crate_gun();
        public abstract Crate Create_crate_medic();
    }
    class Level1Factory : ItemsFactory
    {
        public override Berry Create_berry()
        {
            return new BlueBerry();
        }
        public override Gun Create_gun()
        {
            return new BlueGun();
        }
        public override MedicKit Create_medic()
        {
            return new BlueMedicKit();
        }
        public override Trap Create_trap()
        {
            return new VisibleTrap();
        }
        public override Crate Create_crate()
        {
            return new Crate();
        }

        public override Crate Create_crate_gun()
        {
            return new Crate(Item.Crate_type.guns);
        }

        public override Crate Create_crate_medic()
        {
            return new Crate(Item.Crate_type.health);
        }
    }
    class Level2Factory : ItemsFactory
    {
        public override Berry Create_berry()
        {
            return new RedBerry();
        }
        public override Gun Create_gun()
        {
            return new RedGun();
        }
        public override MedicKit Create_medic()
        {
            return new RedMedicKit();
        }
        public override Trap Create_trap()
        {
            return new InVisibleTrap();
        }
        public override Crate Create_crate()
        {
            return new Crate();
        }

        public override Crate Create_crate_gun()
        {
            return new Crate(Item.Crate_type.guns);
        }

        public override Crate Create_crate_medic()
        {
            return new Crate(Item.Crate_type.health);
        }
    }
}
