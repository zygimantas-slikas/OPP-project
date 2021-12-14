using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Client.Mediator
{
    public interface IMediator
    {
        void Notify(Player sender, string ev);
        void AddPlayer(Player player);
        //void Update(IEnumerable<string> names, IEnumerable<string> created, List<Player> players1, Dictionary<String, Shape> players_gui, Canvas canvas1);
    }
}
