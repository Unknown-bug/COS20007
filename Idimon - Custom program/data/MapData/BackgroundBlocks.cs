using SplashKitSDK;

namespace Idimon
{
    public class BackgroundBlocks : Block
    {
        public BackgroundBlocks(string type, string imagePath, bool isSolid) : base(type, imagePath, isSolid)
        {
        }

        public override void Interact(List<Idimons> idimons, Inventory inventory)
        {
            // Do nothing
        }
    }
}