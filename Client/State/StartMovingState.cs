using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    public class StartMovingState : IState
    {
        string message;
        private ActionState context;

        public StartMovingState(ActionState context)
        {
            this.message = "Start moving";
            this.context = context;
        }

        public IState NextState()
        {
            return new PickingItemsState(context);
        }

        public string OnChange()
        {
            return message;
        }
    }
}
