using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private Locations _location;

        public Player(string name, string desc) : base(new string[] { "me", "inventory" }, name, desc)
        {
            _inventory = new Inventory(name + "'s inventory");
        }

        public GameObject? Locate(string id)
        {
            GameObject? res = null;
            if (AreYou(id))
            {
                return this;
            }
            res = _inventory.Fetch(id);
            if(res != null)
            {
                return res;
            }
            if(_location != null)
            {
                res = _location.Locate(id);
                return res;
            }
            return null;
        }

        public override string FullDescription
        {
            get
            {
                return $"You are {Name}, you are carrying:\n{_inventory.ItemList}";
            }
        }

        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }
        }

        public Locations Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }
    }
}
