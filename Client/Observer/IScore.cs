using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Observer
{
    public interface IScore
    {
        void Update(List<Player> player, int health, int points);
    }
}
