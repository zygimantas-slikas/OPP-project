using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server.Chain_of_responsability
{
    class LoggerToGameWindow : ILogger
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

        public LoggerToGameWindow()
        {
            this.CurrentDirectory = Directory.GetCurrentDirectory();
            this.FileName = "LogAction.txt";
            this.FilePath = this.CurrentDirectory + "/" + this.FileName;

        }
        public override void Log(string Messsage, string args, Player p)
        {
            p.AddComment(Messsage);

            if (args == "Movement")
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
                {
                    w.Write("\r\nLog Entry : Movement ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    w.WriteLine("  :{0}", Messsage);
                    w.WriteLine("-----------------------------------------------");
                }
            }
            else if (args == "Action")
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
                {
                    w.Write("\r\nLog Entry : Action ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    w.WriteLine("  :{0}", Messsage);
                    w.WriteLine("-----------------------------------------------");
                }
            }
        }
    }
}
