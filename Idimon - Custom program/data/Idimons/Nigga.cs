using SplashKitSDK;

namespace Idimon
{
    public class Nigga : Idimons
    {
        public Nigga() : base("Nigga", 1, 35, 55, 40, 90, "img\\Idimon\\Nigga.png", "Normal")
        {
            Skills.Add(new Skills("Thundershock", 1, 15, "A jolt of electricity."));
            Skills.Add(new Skills("Quick Attack", 1, 10, "A fast physical attack."));
            Skills.Add(new Skills("Thunderbolt", 5, 25, "A powerful electric attack."));
            Skills.Add(new Skills("Iron Tail", 10, 20, "A hard tail attack."));
        }
        public override bool CanEvolve => false;
        public override Idimons Evolve()
        {
            throw new NotImplementedException();
        }
    }
}