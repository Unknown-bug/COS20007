using SplashKitSDK;

namespace DrawingShape
{
    public class MyLine : Shape
    {
        private float _endX, _endY;

        public MyLine() : this(Color.Green)
        {
        }

        public MyLine(Color color) : base(color)
        {
            Color = color;
            _endX = 700;
            _endY = 500;
        }

        public float EndX
        {
            get { return _endX; }
            set { _endX = value; }
        }

        public float EndY
        {
            get { return _endY; }
            set { _endY = value; }
        }

        public override void SaveTo(System.IO.StreamWriter writer)
        {
            writer.WriteLine("Line");
            base.SaveTo(writer);
            writer.WriteLine(_endX);
            writer.WriteLine(_endY);
        }

        public override void LoadFrom(System.IO.StreamReader reader)
        {
            base.LoadFrom(reader);
            _endX = reader.ReadSingle();
            _endY = reader.ReadSingle();
        }

        public override void Draw()
        {
            if(Selected)
            {
                DrawOutline();
            }
            SplashKit.DrawLine(Color, X, Y, _endX, _endY);
        }

        public override void DrawOutline()
        {
            SplashKit.FillCircle(Color.Black, X, Y, 5);
            SplashKit.FillCircle(Color.Black, _endX, _endY, 5);
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.PointOnLine(pt, SplashKit.LineFrom(X, Y, _endX, _endY));
        }

    }
}
