using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class DropCommand : Command
    {
        public DropCommand() : base(new string[] { "drop", "put" })
        {
        }

        public override string Execute(Player p, string[] text)
        {
            IHaveInventory _container;
            if (text.Length != 2 && text.Length != 4)
            {
                return "I don't know how to drop that.";
            }
            else if (text[0].ToLower() != "drop" && text[0].ToLower() != "put")
            {
                return "Error in drop input";
            }
            else if (text.Length == 2)
            {
                return DropIn(text[1], p.Location, p);
            }
            else
            {
                if (text[2] != "in")
                {
                    return "What do you want to drop in?";
                }
                else
                {
                    _container = FetchContainter(p, text[3]);
                    if (_container == null)
                    {
                        return "I can't find the " + text[3];
                    }
                    else
                    {
                        return DropIn(text[1], _container, p);
                    }
                }
            }
        }

        private IHaveInventory FetchContainter(Player p, string containerId)
        {
            IHaveInventory container = p.Locate(containerId) as IHaveInventory;
            if (container == null)
            {
                return null;
            }
            else
            {
                return container;
            }
        }

        private string DropIn(string thingId, IHaveInventory container, Player p)
        {
            Item _thing = p.Inventory.Take(thingId);
            if (_thing == null)
            {
                return "I cannot find the " + thingId + " in " + p.Name + "'s inventory.\n";
            }
            else
            {
                container.Inventory.Put(_thing);
                return "You have put the " + thingId + " in the " + container.Name + ".\n";
            }
        }
    }
}
