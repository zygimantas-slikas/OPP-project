using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.AbstractFactory
{
    public class GoodTile : AFactory<Tile>
    {

        public Tile FactoryMethod(Tile.Tile_type type)
        {
            switch (type)
            {
                case Tile.Tile_type.bush:
                    return new Tile(Tile.Tile_type.bush);
                case Tile.Tile_type.water:
                    return new Tile(Tile.Tile_type.water);
                default:
                    return null;
            }
        }
    }
}
