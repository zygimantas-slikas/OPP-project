namespace Server
{
    public class Tile
    {
        public enum Tile_type{ grass, water, wall, lava, bush};
        public Tile_type Surface { get; }
        public Item Loot { get; set; }
        public bool Player_Standing { get; set; }
        public Tile(Tile_type t = Tile_type.grass)
        {
            this.Surface = t;
            Player_Standing = false;
        }
    }

}
