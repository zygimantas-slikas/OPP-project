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
        public Player _player;
    }

    public class SwitchCommand : ICommand
    {

        public SwitchCommand(Player player)
        {
            _player = player;
        }

        public override void Execute()
        {
            _player.switchItem();
        }

        public override void Undo()
        {
            _player.unSwitchItem();
        }
    }

    public class Invoker
    {
        private Dictionary<string, ICommand> _commands;
        private ICommand _command;
        public Invoker()
        {
            _commands = new Dictionary<string, ICommand>();
        }
        public void SetCommand(ICommand command) => _command = command;
        public void Invoke()
        {
            _commands[_command._player.Name] = _command;
            _command.Execute();
            _command = null;
        }
        public void undo(string playerId)
        {
            ICommand command = _commands.ContainsKey(playerId) ? _commands[playerId] : null;

            if (command != null)
            {
                command.Undo();
                _commands.Remove(playerId);
            }
        }
    }

}
