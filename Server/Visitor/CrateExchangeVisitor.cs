using Client.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Visitor
{
    class CrateExchangeVisitor : IVisitor<int>
    {

        public int VisitComposite(Crate composite, int state)
        {
            return composite.items.Aggregate(state, (st, comp) => comp.Accept(this, st));
        }

        public int VisitLeaf(Item leaf, int state)
        {
            return state;
        }
    }
}
