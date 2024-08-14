using System;
using SplashKitSDK;

namespace Idimon
{
    public abstract class Character
    {
        public string Name { get; set; }
        protected Window _window { get; set; }
        protected Point2D _position;
        public List<Bitmap> AnimationFrames { get; set; }
        protected Dictionary<string, Bitmap[]> _images;
        protected int _currentFrame;
        protected double _animationTimer;
        protected string _currentDirection;
        private const double FrameDuration = 0.25;
        private double _timeSinceLastFrame; // Accumulated time since the last frame

        public Character(string name, List<string> imagePaths, Point2D position, Window window)
        {
            Name = name;
            AnimationFrames = new List<Bitmap>();
            LoadAnimationFrames(imagePaths);
            _currentFrame = 0;
            _animationTimer = 0;
            _position = position;
            _window = window;
            _currentDirection = "down";
            _images = new Dictionary<string, Bitmap[]>();
        }

        private void LoadAnimationFrames(List<string> imagePaths)
        {
            foreach (var path in imagePaths)
            {
                AnimationFrames.Add(SplashKit.LoadBitmap(path, path));
            }
        }

        public abstract void LoadImages();

        public void UpdateDirection(string direction)
        {
            if (_currentDirection != direction)
            {
                _currentDirection = direction;
                _currentFrame = 0; // Reset frame when direction changes
                _timeSinceLastFrame = 0; // Reset frame timer
            }
        }

        public void UpdateAnimation(double deltaTime)
        {
            _animationTimer += deltaTime;
            if (_animationTimer > FrameDuration)
            {
                _currentFrame = (_currentFrame + 1) % AnimationFrames.Count;
                _animationTimer = 0;
            }
        }

        public int CurrentFrame => _currentFrame;

        public virtual void Draw()
        {
            Bitmap currentImage = _images[_currentDirection][_currentFrame];
            SplashKit.DrawBitmap(currentImage, _position.X, _position.Y);
        }

        public void Move(double dx, double dy)
        {
            _position.X += dx;
            _position.Y += dy;
        }
    }
}
