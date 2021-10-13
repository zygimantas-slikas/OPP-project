using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.AbstractFactory;

namespace Server.Builder
{
    class MapBuilder2 : MapBuilder
    {
        public MapBuilder2(int map_size, int level) : base(map_size, level)
        {
        }
        public override void add_lakes()
        {
            //TODO: second builder clss realization
            // add option for plyers to choose
        }
        public override void add_bushes()
        {

        }
        public override void add_lava()
        {

        }
        public override void add_walls()
        {

        }
        public override void fill_grass()
        {

        }
    }
}
