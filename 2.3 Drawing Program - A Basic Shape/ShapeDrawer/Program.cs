using System;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class Program
    {
        public static void Main()
        {
            Window window = new Window("Shape Drawer", 800, 600);
            Shape myShape = new Shape();
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                if(SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    myShape.X = SplashKit.MouseX();
                    myShape.Y = SplashKit.MouseY();
                }
                Point2D pt = SplashKit.MousePosition();
                if (myShape.IsAt(pt) && SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    myShape.Color = SplashKit.RandomRGBColor(255);
                }
                myShape.Draw();
                SplashKit.RefreshScreen(); 
            } while (!window.CloseRequested);
        }
    }
}
