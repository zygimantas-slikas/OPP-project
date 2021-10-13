using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.AbstractFactory;

namespace Server.Builder
{
    class MapBuilder1 : MapBuilder
    {
        public MapBuilder1(int map_size, int level) :base(map_size, level)
        {
        }
        public override void add_lakes()
        {
            var rand = new Random();
            for (int i = 0; i < map_size / 2; i++)
            {
                int x = rand.Next(0, map_size);
                int y = rand.Next(0, map_size);
                Tile t = this.prototype.Clone();
                t.Surface = Tile.Tile_type.water;
                map[x, y] = t;
            }
        }
        public override void add_bushes()
        {
            var rand = new Random();
            for (int i = 0; i < map_size / 2; i++)
            {
                int x = rand.Next(0, map_size);
                int y = rand.Next(0, map_size);
                Tile t = this.prototype.Clone();
                t.Surface = Tile.Tile_type.bush;
                map[x, y] = t;
            }
        }
        public override void add_lava()
        {
            var rand = new Random();
            for (int i = 0; i < map_size / 2; i++)
            {
                int x = rand.Next(0, map_size);
                int y = rand.Next(0, map_size);
                Tile t = this.prototype.Clone();
                t.Surface = Tile.Tile_type.lava;
                map[x, y] = t;
            }
        }
        public override void add_walls()
        {
            int lenght = 6;
            var rand = new Random();
            for (int i = 0; i < map_size / 2; i++)
            {
                int x = rand.Next(lenght / 2, map_size - lenght / 2);
                int y = rand.Next(lenght / 2, map_size - lenght / 2);
                int r = rand.Next(0, 2);
                if (r == 0)
                {
                    for (int j = x - lenght / 2; j < (x + lenght / 2); j++)
                    {
                        Tile t = this.prototype.Clone();
                        t.Surface = Tile.Tile_type.wall;
                        map[j, y] = t;
                    }
                }
                else
                {
                    for (int j = x - lenght / 2; j < (x + lenght / 2); j++)
                    {
                        Tile t = this.prototype.Clone();
                        t.Surface = Tile.Tile_type.wall;
                        map[x, j] = t;
                    }
                }
            }
        }
        public override void fill_grass()
        {
            for (int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    if (map[i, j] == null)
                    {
                        Tile t = this.prototype.Clone();
                        t.Surface = Tile.Tile_type.grass;
                        t.Loot = this.items_factory.Create_berry();
                        map[i, j] = t;
                    }
                }
            }
        }
    }
}
