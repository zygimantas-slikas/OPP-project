using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Decorator
{
    class MediumFireDecorator : Decorator
    {
        public virtual int Damage { get; protected set; }

        public MediumFireDecorator(Item component) : base(component)
        {
            this.Damage = 20;
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
