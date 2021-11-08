using System;

namespace Server
{
    public class Tile : Cloneable
    {
        public enum Tile_type{ grass, water, wall, lava, bush};
        public Tile_type Surface { get; set; }
        public Item Loot { get; set; }
        public String Player_Standing { get; set; }
        public Tile(Tile_type t = Tile_type.grass)
        {
            this.Surface = t;
            Player_Standing = null;
        }
        public Cloneable Clone()
        {
            Tile c = new Tile();
            c.Surface = this.Surface;
            c.Player_Standing = new String(this.Player_Standing);
            if (this.Loot != null)
            {
                c.Loot = (Item)this.Loot.Clone();
            }
            else
            {
                c.Loot = null;
            }
            return c;
        }
    }

}
