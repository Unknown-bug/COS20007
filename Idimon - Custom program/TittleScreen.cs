using SplashKitSDK;

namespace Idimon
{
    public class TitleScreen : Screens
    {
        private List<MenuItem> _menuItems;
        private int _selectedIndex;

        public TitleScreen(Window window) : base(window) { 
            _menuItems = new List<MenuItem>
            {
                new MenuItem("New Game", window.Width / 2 - 50, window.Height / 2 - 50),
                new MenuItem("Load Game", window.Width / 2 - 50, window.Height / 2),
                new MenuItem("Exit", window.Width / 2 - 50, window.Height / 2 + 50)
            };
            _selectedIndex = 0;
            _menuItems[_selectedIndex].IsSelected = true;
        }

        public override void Update() { }

        public override void Draw()
        {
            _window.Clear(Color.Black);
            foreach (var item in _menuItems)
            {
                item.Draw();
            }
        }

        public override void HandleInput()
        {
            if (SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                string selectedOption = _menuItems[_selectedIndex].Text;
                if (selectedOption == "New Game")
                {
                    Game.CurrentScreen = new GameScreen(_window);
                }
                else if (selectedOption == "Load Game")
                {
                    // Implement load game logic
                }
                else if (selectedOption == "Exit")
                {
                    _window.Close();
                }
            }
            else if (SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
            {
                _menuItems[_selectedIndex].IsSelected = false;

                if (SplashKit.KeyTyped(KeyCode.DownKey))
                {
                    _selectedIndex = (_selectedIndex + 1) % _menuItems.Count;
                }
                else if (SplashKit.KeyTyped(KeyCode.UpKey))
                {
                    _selectedIndex = (_selectedIndex - 1 + _menuItems.Count) % _menuItems.Count;
                }

                _menuItems[_selectedIndex].IsSelected = true;
            }
        }
    }
}
