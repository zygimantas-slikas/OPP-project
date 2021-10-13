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
            var rand = new Random();
            for (int i = 0; i < map_size / 4; i++)
            {
                int x = rand.Next(2, map_size-2);
                int y = rand.Next(2, map_size-2);
                for (int j = x - 2; j <= x + 2; j++)
                {
                    for (int k = y - 2; k <= y + 2; k++)
                    {
                        Tile t = this.prototype.Clone();
                        t.Surface = Tile.Tile_type.water;
                        map[j, k] = t;
                    }
                }
                map[x - 2, y - 2].Surface = Tile.Tile_type.grass;
                map[x + 2, y - 2].Surface = Tile.Tile_type.grass;
                map[x - 2, y + 2].Surface = Tile.Tile_type.grass;
                map[x + 2, y + 2].Surface = Tile.Tile_type.grass;
            }
        }
        public override void add_bushes()
        {
            var rand = new Random();
            for (int i = 0; i < map_size / 2; i++)
            {
                int x = rand.Next(1, map_size-1);
                int y = rand.Next(1, map_size-1);
                Tile t = this.prototype.Clone();
                t.Surface = Tile.Tile_type.bush;
                map[x, y] = t;

                t = this.prototype.Clone();
                t.Surface = Tile.Tile_type.bush;
                map[x-1, y] = t;
                t = this.prototype.Clone();
                t.Surface = Tile.Tile_type.bush;
                map[x+1, y] = t;
                t = this.prototype.Clone();
                t.Surface = Tile.Tile_type.bush;
                map[x, y-1] = t;
                t = this.prototype.Clone();
                t.Surface = Tile.Tile_type.bush;
                map[x, y+1] = t;
            }
        }
        public override void add_lava()
        {
            var rand = new Random();
            for (int i = 0; i < map_size / 2; i++)
            {
                int x = rand.Next(1, map_size-1);
                int y = rand.Next(1, map_size-1);
                for(int j = x-1; j<= x+1; j++)
                {
                    for (int k = y-1; k <= y+1; k++)
                    {
                        Tile t = this.prototype.Clone();
                        t.Surface = Tile.Tile_type.lava;
                        map[j, k] = t;
                    }
                }
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
            var rand = new Random();
            for (int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    if (map[i, j] == null)
                    {
                        int prob = rand.Next(0, 100);
                        Tile t = this.prototype.Clone();
                        t.Surface = Tile.Tile_type.grass;
                        if (0 <= prob && prob <= 5)
                        {
                            t.Loot = this.items_factory.Create_berry();
                        }
                        else if (10 <= prob && prob <= 15)
                        {
                            t.Loot = this.items_factory.Create_gun();
                        }
                        else
                        {
                            t.Loot = null;
                        }
                        map[i, j] = t;
                    }
                }
            }
        }
    }
}
