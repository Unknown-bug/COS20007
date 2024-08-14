using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Idimon
{
    public class GameMenu : Menu
    {
        private enum MenuState
        {
            Idimon,
            Inventory,
            Team,
            Save,
            Load,
            Setting
        }

        private int _selectedIndex, _idimonIndex, _newIdimonIndex;
        private MenuState _currentMenu;
        private InventoryMenu _inventoryMenu;
        private IdimonMenu _idimonMenu;
        Player _player;

        public GameMenu(Player player, Window window) : base(window)
        {
            _menuItems = new List<MenuItem>
            {
                new MenuItem("Idimon", 50, 60),
                new MenuItem("Inventory", 50, 100),
                new MenuItem("Team", 50, 140),
                new MenuItem("Save", 50, 180),
                new MenuItem("Load", 50, 220),
                new MenuItem("Setting", 50, 260)
            };
            _player = player;
            _selectedIndex = 0;
            _idimonIndex = 99;
            _newIdimonIndex = 99;
            _menuItems[_selectedIndex].IsSelected = true;
            _currentMenu = (MenuState)_selectedIndex;
            _inventoryMenu = new InventoryMenu(_player, _window, "Items");
            _idimonMenu = new IdimonMenu(_player.Inventory.Idimons[0] ,_window, "Status");
        }

        public void Navigate(KeyCode key)
        {
            if (!_visible) return;
            if (_inventoryMenu.Visible || _idimonMenu.Visible) return;
            if (_inventoryMenu.SelectedMenu == "Idimons")
            {
                if(_player.Inventory.Idimons.Count == 0)
                {
                    return;
                }

                _player.Inventory.Idimons[_idimonIndex].IsSelected = false;

                if (key == KeyCode.DownKey)
                {
                    _idimonIndex = (_idimonIndex + 1) % _player.Inventory.Idimons.Count;
                }
                else if (key == KeyCode.UpKey)
                {
                    _idimonIndex = (_idimonIndex - 1 + _player.Inventory.Idimons.Count) % _player.Inventory.Idimons.Count;
                }

                _player.Inventory.Idimons[_idimonIndex].IsSelected = true;
                return;
            }

            if (_inventoryMenu.SelectedMenu == "Team")
            {
                if(_player.Inventory.Idimons.Count == 0)
                {
                    return;
                }

                _player.Inventory.Idimons[_idimonIndex].IsSelected = false;

                if (key == KeyCode.DownKey)
                {
                    _idimonIndex = (_idimonIndex + 1) % _player.Inventory.Idimons.Count;
                    if(_idimonIndex == _newIdimonIndex)
                    {
                        _idimonIndex = (_idimonIndex + 1) % _player.Inventory.Idimons.Count;
                    }
                }
                else if (key == KeyCode.UpKey)
                {
                    _idimonIndex = (_idimonIndex - 1 + _player.Inventory.Idimons.Count) % _player.Inventory.Idimons.Count;
                    if(_idimonIndex == _newIdimonIndex)
                    {
                        _idimonIndex = (_idimonIndex - 1 + _player.Inventory.Idimons.Count) % _player.Inventory.Idimons.Count;
                    }
                }

                _player.Inventory.Idimons[_idimonIndex].IsSelected = true;
                return;
            }

            _menuItems[_selectedIndex].IsSelected = false;

            if (key == KeyCode.DownKey)
            {
                _selectedIndex = (_selectedIndex + 1) % _menuItems.Count;
            }
            else if (key == KeyCode.UpKey)
            {
                _selectedIndex = (_selectedIndex - 1 + _menuItems.Count) % _menuItems.Count;
            }

            _menuItems[_selectedIndex].IsSelected = true;
            _currentMenu = (MenuState)_selectedIndex;
        }

        public void Select()
        {
            if (!_visible) return;
            if (_inventoryMenu.Visible || _idimonMenu.Visible) return;

            switch (_currentMenu)
            {
                case MenuState.Idimon:
                    // Idimon logic
                    _idimonIndex = 0;
                    _inventoryMenu.SelectedMenu = "Idimons";
                    break;
                case MenuState.Inventory:
                    // Inventory logic
                    _inventoryMenu.SelectedMenu = "Items";
                    _inventoryMenu.Open();
                    // Open();
                    break;
                case MenuState.Team:
                    // Team logic
                    _idimonIndex = 0;
                    
                    _inventoryMenu.SelectedMenu = "Team";
                    break;
                case MenuState.Save:
                    // Save game logic
                    break;
                case MenuState.Load:
                    // Load game logic
                    break;
                case MenuState.Setting:
                    // Setting logic
                    break;
            }
        }

        public override void Draw()
        {
            if (!_visible) return;

            if(_inventoryMenu.Visible)
            {
                if(SplashKit.KeyTyped(KeyCode.XKey))
                {
                    _inventoryMenu.Toggle();
                    // Toggle();
                }
                _inventoryMenu.Draw();
                return;
            }
            else if(_idimonMenu.Visible)
            {
                if(SplashKit.KeyTyped(KeyCode.XKey))
                {
                    _idimonMenu.Toggle();
                    // Toggle();
                }
                _idimonMenu.Draw();
                return;
            }
            else
            {
                SplashKit.FillRectangle(Color.RGBAColor(0, 0, 0, 125), 0, 0, SplashKit.ScreenWidth(), SplashKit.ScreenHeight());
                foreach (var item in _menuItems)
                {
                    item.Draw();
                }

                SplashKit.FillRectangle(Color.RGBAColor(255, 255, 255, 255), 205, 0, 2, SplashKit.ScreenHeight());

                // Draw Idimons
                double x = 300;
                double y = 50;
                int i = 0;

                foreach (var idimon in _player.Inventory.Idimons)
                {    
                    if (i == _idimonIndex )
                    {
                        SplashKit.FillRectangle(Color.RGBAColor(255, 255, 0, 150), x - 5, y - 5, SplashKit.ScreenWidth() - x , 80);
                    }
                    if( _newIdimonIndex != 99 && i == _newIdimonIndex)
                    {
                        if (_player.Inventory.Idimons[_newIdimonIndex].IsSelected)
                        {
                            SplashKit.FillRectangle(Color.RGBAColor(255, 255, 0, 150), x - 5, y - 5, SplashKit.ScreenWidth() - x , 80);
                        }
                    }

                    idimon.Draw(x, y);

                    // Details
                    SplashKit.DrawText(idimon.Name, Color.White, "Arial", 30, x + 150, y);
                    SplashKit.DrawText($"Level: {idimon.Level}", Color.White, "Arial", 30, SplashKit.ScreenWidth() - 170, y);
                    idimon.DrawHPBar(x + 150, y + 44, SplashKit.ScreenWidth() - x - 170, 20);
            
                    i++;
                    y += SplashKit.ScreenHeight() / 6;
                }
            }
        }

        public override void HandleInput()
        {
            if (_inventoryMenu.Visible)
            {
                _inventoryMenu.HandleInput();
                return;
            }
            else if (_inventoryMenu.SelectedMenu == "Idimons")
            {
                if(_idimonMenu.Visible)
                {
                    _idimonMenu.HandleInput();
                    return;
                }
                else if (SplashKit.KeyTyped(KeyCode.XKey))
                {
                    _inventoryMenu.SelectedMenu = "Items";
                    _idimonIndex = 99;
                    SplashKit.Delay(100);
                }
                else if (SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
                {
                    Navigate(SplashKit.KeyTyped(KeyCode.DownKey) ? KeyCode.DownKey : KeyCode.UpKey);
                }
                else if (SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
                {
                    _idimonMenu = new IdimonMenu(_player.Inventory.Idimons[_idimonIndex], _window, "Status");
                    _idimonMenu.Open();
                }
                return;
            }
            else if (SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.DownKey) ? KeyCode.DownKey : KeyCode.UpKey);
            }
            else if (SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                if(_currentMenu == MenuState.Team)
                {
                    
                    if(_newIdimonIndex == 99)
                    {
                        _newIdimonIndex = _idimonIndex;
                        if(_newIdimonIndex != 99)
                            _player.Inventory.Idimons[_idimonIndex].IsSelected = true;
                    }
                    else
                    {
                        _player.Inventory.ChangeIdimonPosition(_player.Inventory.Idimons[_idimonIndex], _player.Inventory.Idimons[_newIdimonIndex]);
                        _player.Inventory.Idimons[_newIdimonIndex].IsSelected = false;
                        _newIdimonIndex = 99;
                    }
                    Select();
                    return;
                }
                Select();
            }
            if(_inventoryMenu.SelectedMenu == "Team")
            {
                if(SplashKit.KeyTyped(KeyCode.XKey))
                {
                    if(_newIdimonIndex != 99)
                    {
                        _player.Inventory.Idimons[_newIdimonIndex].IsSelected = false;
                        _newIdimonIndex = 99;
                        SplashKit.Delay(100);
                        return;
                    }
                    _inventoryMenu.SelectedMenu = "";
                    _idimonIndex = 99;
                    SplashKit.Delay(100);
                }
                return;
            }
            if (SplashKit.KeyTyped(KeyCode.XKey))
            {
                Toggle();
            }
        }

        public override void Open()
        {
            Toggle();
        }
    }
}
