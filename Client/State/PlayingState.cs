using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    public class PlayingState : IState
    {
        string message;
        private StateContext context;

        public PlayingState(StateContext context)
        {
            this.message = "Players playing";
            this.context = context;
        }

        public IState NextState()
        {
            return new EndgameState(context);
        }

        public string OnChange()
        {
            return message;
        }
    }
}
