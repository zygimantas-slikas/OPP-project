using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Chain_of_responsability
{
    public class LoggerToConsole : ILogger
    {

        public override void Log(string Messsage, string args, Player p)
        {
            if (args == "Info")
            {
                System.Console.WriteLine("Log Entry : Information");
                System.Console.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                System.Console.WriteLine("  :{0}", Messsage);
                System.Console.WriteLine("-----------------------------------------------");
            }
            else if (args == "Warning")
            {
                System.Console.WriteLine("Log Entry : Warning");
                System.Console.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                System.Console.WriteLine("  :{0}", Messsage);
                System.Console.WriteLine("-----------------------------------------------");
            }
            else if (args == "Error")
            {
                System.Console.WriteLine("Log Entry : Error");
                System.Console.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                System.Console.WriteLine("  :{0}", Messsage);
                System.Console.WriteLine("-----------------------------------------------");
            }
            
        }
    }
}
