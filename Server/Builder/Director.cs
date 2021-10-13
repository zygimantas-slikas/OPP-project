using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.AbstractFactory;

namespace Server.Builder
{
    class Director
    {
        protected MapBuilder build;
        public Director(MapBuilder b)
        {
            this.build = b;
        }
        public void Construct()
        {
            build.add_lakes();
            build.add_bushes();
            build.add_lava();
            build.add_walls();
            build.fill_grass();
        }
    }
    abstract class MapBuilder
    {
        protected Tile[,] map;
        protected int map_size;
        protected ItemsFactory items_factory;
        protected Tile prototype;
        public MapBuilder(int map_size, int level)
        {
            this.map_size = map_size;
            map = new Tile[map_size, map_size];
            if (level == 1)
            {
                this.items_factory = new Level1Factory();
            }
            else if (level == 2)
            {
                this.items_factory = new Level2Factory();
            }
            this.prototype = new Tile(Tile.Tile_type.grass);
        }
        virtual public void add_lakes()
        {
        }
        virtual public void add_bushes()
        {
        }
        virtual public void add_lava()
        {
        }
        virtual public void add_walls()
        {
        }
        virtual public void fill_grass()
        {
        }
        public Tile[,] Get_result()
        {
            return map;
        }
    }
}
