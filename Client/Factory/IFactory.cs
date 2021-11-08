using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Factory
{
    public interface IFactory<K>
    {
        public K FactoryMethod(String s);
    }
}
