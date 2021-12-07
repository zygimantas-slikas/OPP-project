using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    public class DroppingTrapsState : IState
    {
        string message;
        private ActionState context;

        public DroppingTrapsState(ActionState context)
        {
            this.message = "Dropping traps";
            this.context = context;
        }

        public IState NextState()
        {
            return new InventoryState(context);
        }

        public string OnChange()
        {
            return message;
        }
    }
}
