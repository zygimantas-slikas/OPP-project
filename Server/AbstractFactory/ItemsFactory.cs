using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.AbstractFactory
{
    abstract class ItemsFactory
    {
        abstract public Berry Create_berry();
        abstract public Gun Create_gun();
        abstract public MedicKit Create_medic();
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
    }
}
