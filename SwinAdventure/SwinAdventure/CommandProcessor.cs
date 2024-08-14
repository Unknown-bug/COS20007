using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class CommandProcessor : Command
    {
        List<Command> _commands;

        public CommandProcessor() : base(new string[] { "commandprocessor" })
        {
            _commands = new List<Command>();
            _commands.Add(new LookCommand());
            _commands.Add(new MoveCommand());
            _commands.Add(new PickCommand());
            _commands.Add(new DropCommand());
            _commands.Add(new InventoryCommand());
        }

        public override string Execute(Player p, string[] text)
        {
            foreach (Command c in _commands)
            {
                if (c.AreYou(text[0]))
                {
                    return c.Execute(p, text);
                }
            }
            return "This command is not availble.";
        }
    }
}
