using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.AbstractFactory
{
    public class NeutralTile : AFactory<Tile>
    {

        public Tile FactoryMethod(Tile.Tile_type type)
        {
            switch (type)
            {
                case Tile.Tile_type.grass:
                    return new Tile();
                case Tile.Tile_type.wall:
                    return new Tile(Tile.Tile_type.wall);
                default:
                    return null;
            }
        }
    }
}
