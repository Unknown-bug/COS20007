using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Idimon
{
    public abstract class Menu
    {
        protected List<MenuItem> _menuItems;
        protected bool _visible;
        protected Window _window;
        
        public Menu(Window window)
        {
            _menuItems = new List<MenuItem>();
            _visible = false;
            _window = window;
        }

        public bool Visible
        {
            get { return _visible; }
        }

        public void Toggle()
        {
            _visible = !_visible;
        }

        public abstract void HandleInput();

        public abstract void Draw();

        public abstract void Open();
    }
}
