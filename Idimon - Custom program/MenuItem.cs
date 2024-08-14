using SplashKitSDK;

public class MenuItem
{
    public string Text { get; private set; }
    public double X { get; set; }
    public double Y { get; set; }
    public bool IsSelected { get; set; }

    public MenuItem(string text, double x, double y)
    {
        Text = text;
        X = x;
        Y = y;
        IsSelected = false;
    }

    public void Draw()
    {
        SplashKit.LoadFont("Arial", "arial.ttf");
        if (IsSelected)
        {
            // Draw border rectangle
            SplashKit.FillRectangle(Color.Black, X - 5, Y - 5, SplashKit.TextWidth(Text, "Arial", 30) + 10, SplashKit.TextHeight(Text, "Arial", 30) + 10);
            
            // Draw selected text
            SplashKit.DrawText(Text, Color.Yellow, "Arial", 30, X, Y);
        }
        else
        {
            SplashKit.DrawText(Text, Color.White, "Arial", 30, X, Y );
        }
    }
}
