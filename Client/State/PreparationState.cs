using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    class PreparationState : IState
    {
        string message;
        private StateContext context;

        public PreparationState(StateContext context)
        {
            this.message = "Waiting for players";
            this.context = context;
        }

        public IState NextState()
        {
            return new PlayingState(context);
        }

        public string OnChange()
        {
            return message;
        }
    }
}
