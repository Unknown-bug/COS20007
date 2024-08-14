using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class InventoryCommand : Command
    {
        public InventoryCommand() : base(new string[] { "inventory", "inv" })
        {
        }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length != 1)
            {
                return "I don't know how to use that.";
            }
            return p.Inventory.ItemList;
        }
    }
}
