using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Server.Chain_of_responsability
{
    public abstract class ILogger
    {
        public abstract void Log(string Messsage, string args, Player p);
    }
}
