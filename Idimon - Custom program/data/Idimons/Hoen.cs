using SplashKitSDK;

namespace Idimon
{
    public class Hoen : Idimons
    {
        public Hoen() : base("Hoen", 1, 35, 55, 40, 90, "img\\Idimon\\hoen.jpg", "Legendary")
        {
            Skills.Add(new Skills("Nhại", 1, 15, "."));
            Skills.Add(new Skills("N Word", 1, 10, "A fast emotion attack."));
        }
        public Hoen(int level, int health, int attack, int defense, int speed) : base("Hoen", level, health, attack, defense, speed, "img\\Idimon\\hoen.jpg", "Mythical")
        {
            Skills.Add(new Skills("NH words", 1, 15, "."));
            Skills.Add(new Skills("N Word", 1, 10, "A fast emotion attack."));
            Skills.Add(new Skills("Mom jokes", 1, 30, "An emotion attack."));
            Skills.Add(new Skills("Racisms", 1, 40, "A Heavy emotion attack."));
        }

        public override bool CanEvolve => false;
        public override Idimons Evolve()
        {
            throw new NotImplementedException();
        }
    }
}