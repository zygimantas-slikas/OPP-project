﻿using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    public abstract class Decorator : Item
    {
        protected Item wrapee;
        public Decorator(Item component)
        {
            this.wrapee = component;
        }

        public override void PickupEffect(Player p)
        {
            wrapee.PickupEffect(p);
        }

        public override int GetNumber()
        {
            return wrapee.GetNumber();
        }
    }
}
