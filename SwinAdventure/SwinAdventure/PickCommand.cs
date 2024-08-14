using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class PickCommand : Command
    {
        public PickCommand() : base(new string[] { "pickup", "take" })
        {
        }

        public override string Execute(Player p, string[] text)
        {
            IHaveInventory _container;
            if (text.Length != 2 && text.Length != 4)
            {
                return "I don't know how to pick that.";
            }
            else if (text[0].ToLower() != "pickup" && text[0].ToLower() != "take")
            {
                return "Error in pick input";
            }
            else if (text.Length == 2)
            {
                return PickIn(text[1], p.Location, p);
            }
            else
            {
                if (text[2] != "in")
                {
                    return "What do you want to pick in?";
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
                        return PickIn(text[1], _container, p);
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

        private string PickIn(string thingId, IHaveInventory container, Player p)
        {
            Item _thing = container.Inventory.Take(thingId);
            if (_thing == null)
            {
                return "I cannot find the " + thingId + " in the " + container.Name + ".\n";
            }
            else
            {
                p.Inventory.Put(_thing);
                return "You have taken the " + _thing.Name + " from " + container.Name + ".\n";
            }
        }
    }
}
