using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Observer
{
    class Score : IScore
    {
        public void Update(List<Player> player, int health, int points)
        {
            foreach (Player p in player)
            {
                p.Health += health;
                p.Points += points;
            }

        }
    }
}
