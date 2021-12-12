using Client.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Visitor
{
    public interface IVisitor<T>
    {
        T VisitComposite(Crate composite, T state);
        T VisitLeaf(Item leaf, T state);
    }
}
