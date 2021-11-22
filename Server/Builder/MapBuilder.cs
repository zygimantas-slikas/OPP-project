using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.AbstractFactory;

namespace Server.Builder
{
    public abstract class MapBuilder
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
            //this.prototype.Loot = new RedBerry();
        }
        protected virtual void add_lakes()
        {
        }
        protected virtual void add_bushes()
        {
        }
        protected virtual void add_lava()
        {
        }
        protected virtual void add_walls()
        {
        }
        protected virtual void fill_grass()
        {
        }
        protected abstract bool isAdded();
        public Tile[,] Get_result()
        {
            return map;
        }

        public virtual void TemplateAdd()
        {
        }
    }
}
