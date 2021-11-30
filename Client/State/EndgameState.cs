using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    class EndgameState : IState
    {
        string message;
        private StateContext context;

        public EndgameState(StateContext context)
        {
            this.message = "Game over";
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
