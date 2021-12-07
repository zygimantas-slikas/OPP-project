using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    public class ActionState
    {
        private IState state;
        public string message;

        public ActionState()
        {
            this.state = null;
        }

        public void setState(IState state)
        {
            this.state = state;
            message = state.OnChange();
        }

        public void NextState()
        {
            this.state = state.NextState();
            message = state.OnChange();
        }

        public IState getState()
        {
            return state;
        }
    }
}
