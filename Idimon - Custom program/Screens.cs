using System;
using SplashKitSDK;

namespace Idimon
{
    public abstract class Screens
    {
        protected Window _window;

        public Screens(Window window)
        {
            _window = window;
        }

        public abstract void Update();
        public abstract void Draw();
        public abstract void HandleInput();
    }
}
