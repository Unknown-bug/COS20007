using SplashKitSDK;

namespace Idimon
{
    public class Game
    {
        private Window _window;
        public static Screens CurrentScreen { get; set; }

        public Game()
        {
            _window = new Window("Idimon", 960, 768);
            CurrentScreen = new TitleScreen(_window);
        }

        public void Run()
        {
            do
            {
                SplashKit.ProcessEvents();

                CurrentScreen.HandleInput();
                CurrentScreen.Update();
                CurrentScreen.Draw();
                SplashKit.RefreshScreen();
                
                // Check for right-click and get mouse position
                CheckRightClick();
            } while (!_window.CloseRequested);
        }

        private void CheckRightClick()
        {
            if (SplashKit.MouseClicked(MouseButton.RightButton))
            {
                Point2D mousePosition = SplashKit.MousePosition();
                // You can now use mousePosition.X and mousePosition.Y
                System.Console.WriteLine($"Right-click at ({mousePosition.X}, {mousePosition.Y})");
            }
        }
    }
}
