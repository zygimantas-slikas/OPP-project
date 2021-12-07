using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Chain_of_responsability
{
    class LoggerToGameWindowChat : ILogger
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

        public LoggerToGameWindowChat()
        {
            this.CurrentDirectory = Directory.GetCurrentDirectory();
            this.FileName = "LogChat.txt";
            this.FilePath = this.CurrentDirectory + "/" + this.FileName;

        }
        public override void Log(string Messsage, string args, Player p)
        {
            p.AddComment(Messsage);

            if (args == "Chat")
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
                {
                    w.Write("\r\nLog Entry : Chat ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    w.WriteLine("  :{0}", Messsage);
                    w.WriteLine("-----------------------------------------------");
                }
            }
            else if (args == "Taunt")
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
                {
                    w.Write("\r\nLog Entry : Taunt ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    w.WriteLine("  :{0}", Messsage);
                    w.WriteLine("-----------------------------------------------");
                }
            }
        }
    }
}
