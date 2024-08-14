using SplashKitSDK;

namespace Idimon
{
    public class Dope : Items
    {

        public Dope(string name, string description, string imagePath) : base(name, description, imagePath, 1, false, true, "Items")
        {
        }

        public void Use(Idimons idimon)
        {
            idimon.LevelUp();
        }
    }
}