using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    class InventoryState : IState
    {
        string message;
        private ActionState context;

        public InventoryState(ActionState context)
        {
            this.message = "Using inventory";
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
