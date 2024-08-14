﻿using System;

namespace SwinAdventure
{
    public class Bag : Item, IHaveInventory
    {
        Inventory _inventory = new Inventory();

        public Bag(string[] ids, string name, string desc) : base(ids, name, desc)
        {
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            return _inventory.Fetch(id);
        }

        public string FullDescription
        {
            get
            {
                return "In the " + Name + " you can see:\n" + _inventory.ItemList;
            }
        }

        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }
        }
    }
}
