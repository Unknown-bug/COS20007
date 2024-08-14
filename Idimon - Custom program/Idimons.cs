using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Idimon
{
    public abstract class Idimons
    {

        public string Name { get; protected set; }
        public int Level { get; protected set; }
        public int EXP { get; protected set; }
        public int MaxHP { get; protected set; }
        public int CurrentHP { get; set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }
        public int Speed { get; protected set; }
        public Bitmap Image { get; protected set; }
        public int ExperienceToNextLevel { get; protected set; }
        public List<Skills> Skills { get; protected set; }
        public bool IsSelected { get; set; }
        public string Rank { get; set; }

        public Idimons(string name, int level, int maxHP, int attack, int defense, int speed, string imagePaths, string rank )
        {
            Name = name;
            Level = level;
            EXP = 0;
            MaxHP = maxHP;
            CurrentHP = maxHP;
            Attack = attack;
            Defense = defense;
            Speed = speed;
            Skills = new List<Skills>();
            Image = SplashKit.LoadBitmap(name, imagePaths);
            ExperienceToNextLevel = CalculateExperienceToNextLevel();
            Rank = rank;
            // CanEvolve = canEvolve;
        }
        
        private int CalculateExperienceToNextLevel()
        {
            return (int)(Level * Level * 10);
        }

        public void UseMove(int moveIndex, Idimons opponent)
        {
            if (moveIndex >= 0 && moveIndex < Skills.Count && Level >= Skills[moveIndex].LevelRequirement)
            {
                Skills[moveIndex].Use(this, opponent);
            }
            else
            {
                Console.WriteLine($"{Name} cannot use this move.");
            }
        }

        public void GainExperience(int exp)
        {
            EXP += exp;
            if (EXP >= ExperienceToNextLevel)
            {
                LevelUp();
            }
        }

        public virtual bool CanEvolve { get; }
        public abstract Idimons Evolve();        

        public void LevelUp()
        {
            Level++;
            EXP = 0;
            MaxHP += 10;
            CurrentHP = MaxHP;
            Attack += 2;
            Defense += 2;
            Speed += 1;
            ExperienceToNextLevel = CalculateExperienceToNextLevel();
        }

        public void TakeDamage(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP < 0)
            {
                CurrentHP = 0;
            }
        }

        public bool IsFainted()
        {
            return CurrentHP <= 0;
        }

        public void Heal(int amount)
        {
            CurrentHP += amount;
            if (CurrentHP > MaxHP)
            {
                CurrentHP = MaxHP;
            }
        }

        public void Draw(double x, double y)
        {
            SplashKit.DrawBitmap(Image, x, y);
        }

        public void DrawHPBar(double x, double y, double width, double height)
        {
            SplashKit.DrawRectangle(Color.White, x, y, width, height);
            if(CurrentHP > 0)
                SplashKit.FillRectangle(Color.Red, x + 1, y + 1, width * CurrentHP / MaxHP - 2 , height - 2);
        }

        public void Attacking(Idimons opponent, int moveIndex)
        {
            if (moveIndex >= 0 && moveIndex < Skills.Count)
            {
                Console.WriteLine(Skills[moveIndex].Damage);
                Skills[moveIndex].Use(this, opponent);
            }
            else
            {
                Console.WriteLine($"{Name} cannot use this move.");
            }
        }

        public bool IsFasterThan(Idimons opponent)
        {
            return this.Speed > opponent.Speed;
        }

        public void Faint()
        {
            Console.WriteLine($"{Name} has fainted!");
            // Additional logic for fainting
        }
    }
}
