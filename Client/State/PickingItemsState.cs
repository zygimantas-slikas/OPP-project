using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    public class PickingItemsState : IState
    {
        string message;
        private ActionState context;

        public PickingItemsState(ActionState context)
        {
            this.message = "Picking items";
            this.context = context;
        }

        public IState NextState()
        {
            return new DroppingTrapsState(context);
        }

        public string OnChange()
        {
            return message;
        }
    }
}
