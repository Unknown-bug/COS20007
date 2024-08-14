using SplashKitSDK;

namespace Idimon
{
    public class Beaver : Idimons
    {
        public Beaver() : base("Beaver", 1, 35, 55, 40, 90, "img\\Idimon\\Beaver.png", "Legendary")
        {
            Skills.Add(new Skills("N Word", 1, 10, "A fast physical attack."));
        }

        public override bool CanEvolve => this.Level >= 10;
        public override Idimons Evolve()
        {
            if (CanEvolve)
            {
                return new Hoen(this.Level, this.MaxHP, this.Attack, this.Defense, this.Speed);
            }
            return this;
        }
    }
}