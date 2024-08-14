using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Idimon
{
    // The Inventory class manages the items and Idimons a player has.
    public class Inventory
    {
        private Dictionary<string, Items> _items; // Dictionary to store items by name
        private List<Idimons> _idimons; // List to store Idimons
        private int _selectedIndex; // Index of the currently selected item
        private bool _visible; // Visibility status of the inventory

        // Constructor to initialize the inventory
        public Inventory()
        {
            _items = new Dictionary<string, Items>();
            _idimons = new List<Idimons>();
            _selectedIndex = 0;
            _visible = false;
        }

        // Property to get or set the visibility of the inventory
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        // Toggle the visibility of the inventory
        public void Toggle()
        {
            _visible = !_visible;
        }

        // Add an item to the inventory
        public void AddItem(Items item)
        {
            if (_items.ContainsKey(item.Name))
            {
                _items[item.Name].Quantity += item.Quantity;
            }
            else
            {
                _items[item.Name] = item;
            }
        }

        // Add an Idimon to the inventory
        public void AddIdimon(Idimons idimon)
        {
            if (_idimons.Count < 6)
            {
                _idimons.Add(idimon);
            }
        }

        // Remove one quantity of an item from the inventory
        public void RemoveItem(string itemName)
        {
            if (_items.ContainsKey(itemName))
            {
                _items[itemName].Quantity -= 1;

                if (_items[itemName].Quantity <= 0)
                {
                    _items.Remove(itemName);
                }
            }
        }

        // Remove an Idimon from the inventory
        public void RemoveIdimon(Idimons idimon)
        {
            _idimons.Remove(idimon);
        }

        // Find an item by its name
        public Items? FindItemByName(string name)
        {
            if (_items.ContainsKey(name))
            {
                return _items[name];
            }
            return null;
        }

        // Get a list of all items of a specific type
        public List<Items> GetAllItems(string type)
        {
            List<Items> items = new List<Items>();
            foreach (Items item in _items.Values)
            {
                if (item.Type == type)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        // Display the inventory on the screen
        public void DisplayInventory(Window window, double startX, double startY, string type)
        {
            double x = startX;
            double y = startY;
            double X_SPACING = 105 + SplashKit.ScreenWidth() / 3;
            double Y_SPACING = 50;

            List<Items> items = GetAllItems(type);
            int i = 0;

            foreach (Items item in items)
            {
                i += 1;
                item.Draw(window, x, y);
                if (i % 2 != 0)
                {
                    x += X_SPACING;
                }
                else
                {
                    x = startX;
                    y += Y_SPACING;
                }
            }

            // Highlight the selected item
            if (items.Count > 0)
            {
                items[_selectedIndex].IsSelected = true;
            }
        }

        // Overloaded method to display the inventory with custom spacing
        public void DisplayInventory(Window window, double startX, double startY, double x_spacing, string type)
        {
            double x = startX;
            double y = startY;
            double X_SPACING = x_spacing;
            double Y_SPACING = 50;

            List<Items> items = GetAllItems(type);
            int i = 0;

            foreach (Items item in items)
            {
                i += 1;
                item.DrawNoDescription(window, x, y);
                if (i % 2 != 0)
                {
                    x += X_SPACING;
                }
                else
                {
                    x = startX;
                    y += Y_SPACING;
                }
            }

            // Highlight the selected item
            if (items.Count > 0)
            {
                items[_selectedIndex].IsSelected = true;
            }
        }

        // Navigate through the items in the inventory
        public void Navigate(string type)
        {
            List<Items> items = GetAllItems(type);
            if (items.Count == 0) return;

            if (SplashKit.KeyTyped(KeyCode.RightKey))
            {
                items[_selectedIndex].IsSelected = false;
                _selectedIndex = (_selectedIndex + 1) % items.Count;
            }
            else if (SplashKit.KeyTyped(KeyCode.LeftKey))
            {
                items[_selectedIndex].IsSelected = false;
                _selectedIndex = (_selectedIndex - 1 + items.Count) % items.Count;
            }
            else if (SplashKit.KeyTyped(KeyCode.DownKey))
            {
                items[_selectedIndex].IsSelected = false;
                if (items.Count % 2 != 0 && _selectedIndex == items.Count - 1)
                {
                    _selectedIndex = 0;
                    return;
                }
                _selectedIndex = (_selectedIndex + 2 + items.Count) % items.Count;
            }
            else if (SplashKit.KeyTyped(KeyCode.UpKey))
            {
                items[_selectedIndex].IsSelected = false;
                if (items.Count % 2 != 0 && (_selectedIndex == 0 || _selectedIndex == 1))
                {
                    _selectedIndex = items.Count - 1;
                    return;
                }
                _selectedIndex = (_selectedIndex - 2 + items.Count) % items.Count;
            }
        }

        // Handle input for navigating and using items in the inventory
        public string HandleInput(string type)
        {
            List<Items> items = GetAllItems(type);
            if (SplashKit.KeyTyped(KeyCode.XKey))
            {
                if (items.Count == 0)
                {
                    Toggle();
                    SplashKit.Delay(100);
                    return "";
                }
                items[_selectedIndex].IsSelected = false;
                _selectedIndex = 0;
                Toggle();
                SplashKit.Delay(100);
                return "";
            }
            Navigate(type);
            if (SplashKit.KeyTyped(KeyCode.ZKey))
            {
                Items item = items[_selectedIndex];
                if (item.GetType() == typeof(Potion))
                {
                    Potion potion = (Potion)item;
                    potion.Use(_idimons[0]);
                }
                else if (item.GetType() == typeof(Dope))
                {
                    Dope dope = (Dope)item;
                    dope.Use(_idimons[0]);
                }
            }
            return type;
        }

        // Overloaded method to handle input with a BattleScreen parameter
        public string HandleInput(string type, BattleScreen battleScreen)
        {
            List<Items> items = GetAllItems(type);
            if (SplashKit.KeyTyped(KeyCode.XKey))
            {
                if (items.Count == 0)
                {
                    Toggle();
                    SplashKit.Delay(100);
                    return "";
                }
                items[_selectedIndex].IsSelected = false;
                _selectedIndex = 0;
                Toggle();
                SplashKit.Delay(100);
                return "";
            }
            Navigate(type);
            return type;
        }

        // Change the position of two Idimons in the inventory
        public void ChangeIdimonPosition(Idimons currentIdimon, Idimons newIdimon)
        {
            int currentIndex = _idimons.IndexOf(currentIdimon);
            int newIndex = _idimons.IndexOf(newIdimon);

            _idimons[currentIndex] = newIdimon;
            _idimons[newIndex] = currentIdimon;
        }

        // Get the total number of items in the inventory
        public int GetTotalItems()
        {
            return _items.Count;
        }

        // Property to get the list of Idimons in the inventory
        public List<Idimons> Idimons
        {
            get { return _idimons; }
        }

        // Property to get the currently selected item
        public Items SelectedItem
        {
            get { return GetAllItems("Items")[_selectedIndex]; }
        }
    }
}