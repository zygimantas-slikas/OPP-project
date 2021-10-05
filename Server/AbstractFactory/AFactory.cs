using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.AbstractFactory
{
    public interface AFactory<T>
    {
        public T FactoryMethod(Tile.Tile_type type);
    }
}
