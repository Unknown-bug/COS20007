using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Inventory
    {
        List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
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
                string list = "";
                foreach (Item i in _items)
                {
                    list += i.ShortDescription + "\n";
                }
                return list;
            }
        }
    }
}
