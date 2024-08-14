using SplashKitSDK;

namespace Idimon
{
    public abstract class Block
    {
        public string Type { get; set; }
        public Bitmap Image { get; set; }
        public bool IsSolid { get; set; }

        public Block(string type, string imagePath, bool isSolid)
        {
            Type = type;
            Image = SplashKit.LoadBitmap(type, imagePath);
            IsSolid = isSolid;
        }
        
        public abstract void Interact(List<Idimons> idimons, Inventory inventory);

        public void Draw(Window window, double x, double y)
        {
            window.DrawBitmap(Image, x, y);
        }
    }
}
