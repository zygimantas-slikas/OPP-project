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
    }
}
