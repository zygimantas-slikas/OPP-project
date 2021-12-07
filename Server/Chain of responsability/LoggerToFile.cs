using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server.Chain_of_responsability
{
    public class LoggerToFile : ILogger
    {
        private String CurrentDirectory
        {
            get;
            set;
        }

        private String FileName
        {
            get;
            set;
        }

        private String FilePath
        {
            get;
            set;
        }

        public LoggerToFile()
        {
            this.CurrentDirectory = Directory.GetCurrentDirectory();
            this.FileName = "Log.txt";
            this.FilePath = this.CurrentDirectory + "/" + this.FileName;

        }

        public override void Log(string Messsage, string args, Player p)
        {

            System.Console.WriteLine("Logged : {0}", Messsage);

            if (args == "Info")
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
                {
                    w.Write("\r\nLog Entry : Information ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    w.WriteLine("  :{0}", Messsage);
                    w.WriteLine("-----------------------------------------------");
                }
            }
            else if (args == "Warning")
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
                {
                    w.Write("\r\nLog Entry : Warning ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    w.WriteLine("  :{0}", Messsage);
                    w.WriteLine("-----------------------------------------------");
                }
            }
            else if (args == "Error")
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
                {
                    w.Write("\r\nLog Entry : Error ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    w.WriteLine("  :{0}", Messsage);
                    w.WriteLine("-----------------------------------------------");
                }
            }
        }
    }
}
