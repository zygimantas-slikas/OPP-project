using System;
using System.Windows.Shapes;

namespace Client
{
    public class Tile
    {
        public enum Tile_type { grass, water, wall, lava, bush };
        public Tile_type Surface { get; set; }
        public Item Loot { get; set; }
        public String Player_Standing { get; set; }
        public Rectangle Icon { get; set; }
        public Tile(Tile_type t = Tile_type.grass)
        {
            this.Surface = t;
            Player_Standing = null;
        }
        public Tile()
        {
            Player_Standing = null;
        }
        
    }

}
