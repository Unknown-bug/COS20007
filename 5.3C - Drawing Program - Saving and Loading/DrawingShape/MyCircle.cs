using SplashKitSDK;

namespace DrawingShape
{
    internal class MyCircle : Shape
    {
        int _radius;
        public MyCircle() : base(color: Color.Green)
        {
            _radius = 50;
        }

        public MyCircle(Color color, int x, int y, int radius) : base(color)
        {
            Color = color;
            X = x;
            Y = y;
            _radius = radius;
        }

        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Circle");
            base.SaveTo(writer);
            writer.WriteLine(_radius);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            _radius = reader.ReadInteger();
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.FillCircle(Color, X, Y, _radius);
        }

        public override void DrawOutline()
        {
            SplashKit.FillCircle(Color.Black, X, Y, _radius+2);
        }

        public override bool IsAt(Point2D pt)
        {
            double a = (double)(pt.X - X);
            double b = (double)(pt.Y - Y);
            if (Math.Sqrt(a * a + b * b) < _radius)
            {
                return true;
            }
            return false;
        }
    }
}
