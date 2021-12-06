using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Iterator
{
    public interface IAggregate<T>
    {
        IIterator<T> GetIterator();
        T this[int itemIndex] { set; get; }
        int Count();
    }
}
