using SplashKitSDK;

namespace DrawingShape
{
    internal class MyRectangle : Shape
    {
        private int _width, _height;

        public MyRectangle() : base(color: Color.Green)
        {
            _width = 100;
            _height = 100;
        }

        public MyRectangle(Color color, int x, int y, int width, int height) : base(color)
        {
            Color = color;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.FillRectangle(Color, X, Y, Width, Height);
        }

        public override void DrawOutline()
        {
            SplashKit.FillRectangle(Color.Black, X - 2, Y - 2, Width + 4, Height + 4);
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.PointInRectangle(pt, SplashKit.RectangleFrom(X, Y, _width, _height));
        }
    }
}
