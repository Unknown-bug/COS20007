using System;

namespace SwinAdventure
{
    public class Inventory
    {
        List<Item> _items;
        string _name;

        public Inventory(string name)
        {
            _items = new List<Item>();
            _name = name;
        }

        public bool HasItem(string id)
        {
            foreach (Item i in _items)
            {
                if(i.AreYou(id))
                {
                    return true;
                }
            }
            return false;
        }

        public void Put(Item itm)
        {
            _items.Add(itm);
        }
        
        public Item? Take(string id)
        {
            Item? t = null;
            foreach (Item i in _items)
            {
                if(i.AreYou(id))
                {
                    t = i;
                    _items.Remove(i);
                    return t;
                }
            }
            return t;
        }

        public Item? Fetch(string id)
        {
            Item? t = null;
            foreach (Item i in _items)
            {
                if (i.AreYou(id))
                {
                    return i;
                }
            }
            return t;
        }

        public string ItemList
        {
            get
            {
                if (_items.Count == 0)
                {
                    return _name + " is empty.";
                }
                string list = "";
                foreach (Item i in _items)
                {
                    list += "\t" + i.ShortDescription + "\n";
                }
                return list;
            }
        }
    }
}
