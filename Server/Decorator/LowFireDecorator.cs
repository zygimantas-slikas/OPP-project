using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Decorator
{
    class LowFireDecorator : Decorator
    {
        public virtual int Damage { get; protected set; }

        public LowFireDecorator(Item component) : base(component)
        {
            this.Damage = 10;
        }

        public override void PickupEffect(Player p)
        {
            base.PickupEffect(p);
            p.AddDamage(this.Damage);
        }

        public override Item Clone()
        {
            throw new NotImplementedException();
        }
    }
}
