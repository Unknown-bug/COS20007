using SplashKitSDK;

namespace Idimon
{
    public class HealerBlocks : Block
    {
        public HealerBlocks(string type, string imagePath, bool isSolid) : base(type, imagePath, isSolid)
        {
        }

        public override void Interact(List<Idimons> idimons, Inventory inventory)
        {
            foreach (var idimon in idimons)
            {
                idimon.CurrentHP = idimon.MaxHP;
            }
        }
    }
}