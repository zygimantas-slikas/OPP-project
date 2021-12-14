using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Memento
{
    class Caretaker
    {
        List<Memento> memento = new List<Memento>();
        public Memento Memento
        {
            set { memento.Add(value); }
            get { return memento[0]; }
        }
    }
}