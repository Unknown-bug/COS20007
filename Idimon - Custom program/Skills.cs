using SplashKitSDK;

namespace Idimon
{
    // The Skills class represents a skill that an Idimon can use in the game.
    public class Skills
    {
        // Properties of the skill
        public string Name { get; private set; } // Name of the skill
        public int LevelRequirement { get; private set; } // Minimum level required to use the skill
        public int Damage { get; private set; } // Damage dealt by the skill
        public string Description { get; private set; } // Description of the skill

        // Constructor to initialize the skill with its properties
        public Skills(string name, int levelRequirement, int damage, string description)
        {
            Name = name;
            LevelRequirement = levelRequirement;
            Damage = damage;
            Description = description;
        }

        // Method to use the skill in a battle
        public void Use(Idimons user, Idimons opponent)
        {
            // Calculate the damage dealt to the opponent
            int damageDealt = Damage * user.Attack / opponent.Defense;
            if (damageDealt < 0) damageDealt = 0; // Ensure damage is not negative
            opponent.TakeDamage(damageDealt); // Apply damage to the opponent

            // Draw the user Idimon at coordinates (100, 100)
            user.Draw(100, 100);
        }

        // Method to display the skill's information
        public void Display()
        {
            // Print the skill's name, level requirement, description, and damage
            Console.WriteLine($"{Name} (Level {LevelRequirement}): {Description}. Damage: {Damage}");
        }
    }
}