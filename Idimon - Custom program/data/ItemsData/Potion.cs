using SplashKitSDK;

namespace Idimon
{
    public class Potion : Items
    {
        private int _recoverAmount;
        public Potion(string name, string description, string imagePath, int recoverAmount) : base(name, description, imagePath, 1, false, true, "Items")
        {
            _recoverAmount = recoverAmount;
        }

        public void Use(Idimons idimon)
        {
            idimon.CurrentHP += _recoverAmount;
            if (idimon.CurrentHP > idimon.MaxHP)
            {
                idimon.CurrentHP = idimon.MaxHP;
            }
        }
    }
}