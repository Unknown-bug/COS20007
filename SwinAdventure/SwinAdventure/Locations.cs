using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Locations : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private string _name, _desc;
        List<Paths> _paths;

        public Locations(string name, string desc) : base(new string[] { "location" }, name, desc)
        {
            _inventory = new Inventory(name);
            _paths = new List<Paths>();
            _desc = desc;
        }

        public Locations(string name, string desc, List<Paths> paths) : this(name, desc)
        {
            _paths = paths;
        }

        public string PathsList
        {
            get
            {
                if (_paths.Count == 0)
                {
                    return "There are no paths to move to.";
                }

                string list = "There are exit to ";
                int i = 0;
                foreach (Paths p in _paths)
                {
                    i++;
                    if(i!=1)
                    {
                        list += ", ";
                    }
                    list += "the " + p.FirstId;
                }

                return list;
            }
        }

        public GameObject? Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }

            foreach (Paths p in _paths)
            {
                if (p.AreYou(id))
                {
                    return p;
                }
            }

            return _inventory.Fetch(id);
        }

        public override string FullDescription
        {
            get
            {
                return "You are in the " + Name + ".\n" + _desc + "\n" + PathsList + "\nIn this room you can see:\n" + _inventory.ItemList;
            }
        }

        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }
        }

        public void AddPath(Paths path)
        {
            _paths.Add(path);
        }
    }
}
