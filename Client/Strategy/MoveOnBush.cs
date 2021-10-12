using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace Client.Strategy
{
    class MoveOnBush : IMovementStrategy
    {


        public override void Move()
        {
            GameSettings settings = GameSettings.GetInstance();
            settings.SetSpeed(300);
        }
    }
}
