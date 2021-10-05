using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.AbstractFactory
{
    public class BadTile : AFactory<Tile>
    {

        public Tile FactoryMethod(Tile.Tile_type type)
        {
            switch (type)
            {
                case Tile.Tile_type.lava:
                    return new Tile(Tile.Tile_type.lava);
                default:
                    return null;
            }
        }
    }
}
