using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class LookCommand : Command
    {
        public LookCommand() : base(new string[] { "look" }) { }

        public override string Execute(Player p, string[] text)
        {
            IHaveInventory _container;
            if (text.Length != 3 && text.Length != 5)
            {
                return "I don't know how to look like that";
            }
            else if (text[0].ToLower() != "look")
            {
                return "Error in look input";
            }
            else if (text[1].ToLower() != "at")
            {
                return "What do you want to look at?";
            }
            else if (text.Length == 3)
            {
                _container = p;
                return LookAtIn(text[2], _container);
            }
            else
            {
                if (text[3] != "in")
                {
                    return "What do you want to look in?";
                }
                else
                {
                    _container = FetchContainter(p, text[4]);
                    if (_container == null)
                    {
                        return "I can't find the " + text[4];
                    }
                    else
                    {
                        return LookAtIn(text[2], _container);
                    }
                }
            }
        }

        private IHaveInventory FetchContainter( Player p, string containerId)
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

        private string LookAtIn(string thingId, IHaveInventory container)
        {
            GameObject obj = container.Locate(thingId);
            if (obj == null)
            {
                return "I can't find the " + thingId;
            }
            else
            {
                return obj.FullDescription;
            }
        }
    }
}
