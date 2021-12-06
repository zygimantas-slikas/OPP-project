using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Iterator
{
    public interface IIterator<T>
    {
        T First();
        T Next();
        T Current();
        bool HasNext();
    }
}
