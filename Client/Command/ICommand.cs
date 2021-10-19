using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Command
{
    public abstract class ICommand
    {
        public abstract void Execute();
        public abstract void Undo();
    }

    public class TakeCommand : ICommand
    {
        private readonly Item _item;
        private List<string> _itemList;

        public TakeCommand(Item item, List<string> itemlist)
        {
            _item = item;
            _itemList = itemlist;
        }

        public override void Execute()
        {
            _itemList.Add(_item.ToString());
        }

        public override void Undo()
        {
            _itemList.Remove(_item.ToString());
        }


    }

    public class Invoker
    {
        private readonly List<ICommand> _commands;
        private ICommand _command;
        public Invoker()
        {
            _commands = new List<ICommand>();
        }
        public void SetCommand(ICommand command) => _command = command;
        public void Invoke()
        {
            _commands.Add(_command);
            _command.Execute();
            _command.Undo();
        }
    }

}
