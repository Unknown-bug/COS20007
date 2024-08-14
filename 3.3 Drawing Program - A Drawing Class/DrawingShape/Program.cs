using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DrawingShape
{
    public class Program
    {
        public static void Main()
        {
            Window window = new Window("Shape Drawer", 800, 600);
            Drawing myDrawing = new Drawing();
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Shape myShape = new Shape();
                    myShape.X = SplashKit.MouseX();
                    myShape.Y = SplashKit.MouseY();
                    myDrawing.AddShape(myShape);
                }
                Point2D pt = SplashKit.MousePosition();
                if(SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    myDrawing.Background = SplashKit.RandomRGBColor(255);
                }
                if(SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    foreach(Shape s in myDrawing.Shapes)
                    {
                        if(s.IsAt(pt))
                        {
                            s.Selected = !s.Selected;
                        }
                    }
                }
                if(SplashKit.KeyTyped(KeyCode.DeleteKey)||SplashKit.KeyTyped(KeyCode.BackspaceKey))
                {
                    for (int i = myDrawing.ShapeCount - 1; i >= 0; i--)
                    {
                        if (myDrawing.Shapes[i].Selected)
                        {
                            myDrawing.RemoveShape(myDrawing.Shapes[i]);
                        }
                    }
                }
                myDrawing.Draw();
                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
    }
}
