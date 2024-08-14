using SplashKitSDK;

namespace Idimon
{
    public class IdimonMenu : Menu
    {
        private enum MenuState
        {
            Summary,
            Equipment,
            Return
        }
        // Window _window;
        private int _width, _length;
        private Point2D _point;
        private List<Items> _items;
        private Idimons _idimon;
        private int _selectedIndex;
        private MenuState _currentMenu;
        private string _type;
        public string SelectedMenu { get; set; }

        public IdimonMenu(Idimons idimon, Window window, string selectedMenu) : base( window)
        { 
            _menuItems = new List<MenuItem>
            {
                new MenuItem("Summary", 20, SplashKit.ScreenHeight() / 3 * 2 + 80),
                new MenuItem("Equipment", 20, SplashKit.ScreenHeight() / 3 * 2 + 130),
                new MenuItem("Return", 20, SplashKit.ScreenHeight() / 3 * 2 + 180),
            };
            _selectedIndex = 0;
            _menuItems[_selectedIndex].IsSelected = true;
            _currentMenu = (MenuState)_selectedIndex;
            _idimon = idimon;
            _items = new List<Items>();
            _type = "";
            SelectedMenu = selectedMenu;
        }

        public void Select()
        {
            if (!_visible) return;

            switch (_currentMenu)
            {
                case MenuState.Summary:
                    // Items logic
                    _type = "Summary";
                    break;
                case MenuState.Equipment:
                    // Equipment logic
                    _type = "Equipment";
                    break;
                case MenuState.Return:
                    // Key Items logic
                    Toggle();
                    _type = "Return";
                    break;
            }
        }

        public override void Draw()
        {
            if (!_visible) return;
            SplashKit.FillRectangle(Color.RGBAColor(38, 35, 67, 140), 10, 5, SplashKit.ScreenWidth() - 20, SplashKit.ScreenHeight() - 10);
            foreach (var item in _menuItems)
            {
                item.Draw();
            }
            if(_type == "Summary")
            {
                _idimon.Draw(300, 130);
                SplashKit.LoadFont("Arial", "arial.ttf");
                SplashKit.DrawText(_idimon.Name, Color.White, "Arial", 30, 300 + 5, 200);

                SplashKit.DrawText("Lv " + _idimon.Level, Color.White, "Arial", 30, 370, 130 + 14 );
                SplashKit.DrawText(_idimon.ExperienceToNextLevel - _idimon.EXP + " til next level " , Color.White, "Arial", 20, 490, 130 + 24);
                SplashKit.DrawRectangle(Color.RGBAColor(255, 255, 255, 255), 370, 130 + 64 - 20, 300, 20);
                if(_idimon.EXP != 0)
                    SplashKit.FillRectangle(Color.Blue, 370 + 1, 130 + 64 - 20 + 1, (300 - 2) * _idimon.EXP / _idimon.ExperienceToNextLevel , 20 - 2);

                SplashKit.DrawText("HP: " + _idimon.CurrentHP + "/" + _idimon.MaxHP, Color.White, "Arial", 20, 300 + 5, 244);
                SplashKit.DrawText("Attack: " + _idimon.Attack, Color.White, "Arial", 20, 300 + 5, 274);
                SplashKit.DrawText("Defense: " + _idimon.Defense, Color.White, "Arial", 20, 300 + 5, 304);
                SplashKit.DrawText("Speed: " + _idimon.Speed, Color.White, "Arial", 20, 300 + 5, 334);

                int i=0;
                double x = 300 + 5;
                double y = 404;
                SplashKit.DrawText("Moves", Color.White, "Arial", 30, 300 + 5, 364);
                foreach (var move in _idimon.Skills)
                {
                    SplashKit.DrawText(move.Name, Color.White, "Arial", 20, x, y);
                    i++;
                    if(i % 2 != 0)
                    {
                        x += 150;
                    }
                    else
                    {
                        x = 300 + 5;
                        y += 30;
                    }
                }

                return;
            }

            foreach (var item in _menuItems)
            {
                item.Draw();
            }
        }

        public void Navigate(KeyCode key)
        {
            if (!_visible) return;

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

        public override void HandleInput()
        {
            if (!_visible) return;

            if (_type == "Summary")
            {
                SummaryHandleInput();
                return;
            }

            if (SplashKit.KeyTyped(KeyCode.XKey))
            {
                _menuItems[_selectedIndex].IsSelected = false;
                _type = "";
                _selectedIndex = 0;
                _currentMenu = (MenuState)_selectedIndex;
                _menuItems[_selectedIndex].IsSelected = true;
                Draw();
                Toggle();
            }
            else if (SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.DownKey) ? KeyCode.DownKey : KeyCode.UpKey);
            }
            else if (SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                Select();
            }
        }

        public void SummaryHandleInput()
        {
            if (!_visible) return;

            if(SplashKit.KeyTyped(KeyCode.XKey))
            {
                _type = "";
                SplashKit.Delay(100);
            }
        }

        public override void Open()
        {
            
            // _window.Refresh(60);
            Toggle();
        }
    }
}