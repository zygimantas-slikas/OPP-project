using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Strategy
{
    public class MoveOnLava : IMovementStrategy
    {
        public override void Move()
        {
            GameSettings settings = GameSettings.GetInstance();
            settings.SetSpeed(50);
        }
    }
}
