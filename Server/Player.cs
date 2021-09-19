namespace Server
{
    public class Player
    {
        public int x, y; //coordinates
        public string Con_id { get; }
        public int Health { get; }
        public Player(string id)
        {
            this.Con_id = id;
            this.Health = 100;
        }
    }

}
