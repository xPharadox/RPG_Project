using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class Enemy : Creature
    {
        public int numOfAttack;


        public Enemy(string _name, int _attack, int _health, int _magic, bool _res)
            : base(_name, _attack, _health, _magic, _res)
        {

        }

        public static List<Enemy> EnemyContainer()
        {
            List<Enemy> enemies = new List<Enemy>();
            enemies.Add(new Enemy("Giant Rat", 1, 1, 1, false));
            enemies.Add(new Enemy("Wolf", 1, 1, 1, false));
            enemies.Add(new Enemy("Harpy", 1, 1, 1, false));
            enemies.Add(new Enemy("Troll", 1, 1, 1, false));
            enemies.Add(new Enemy("Dragon", 1, 1, 1, false));
            return enemies;
        }

        public void EnemyTurn(string name, Player target)
        {
            switch (name)
            {
                case "Giant Rat":
                    RatAI(target);
                    break;
                case "Wolf":
                    WolfAI(target);
                    break;
                case "Harpy":
                    HarpyAI(target);
                    break;
                case "Troll":
                    TrollAI(target);
                    break;
                case "Dragon":
                    DragonAI(target);
                    break;
            }
        }

        public int Randomizer(int min, int max)
        {
            Random ran = new Random();
            return ran.Next(min, max);
        }

        //Enemy AI
        public void RatAI(Player target)
        {
            int AttNum = 2;
            string user = "Giant Rat";
            int ch = Randomizer(1, AttNum);

            switch (ch)
            {
                case 1:
                    Bite(target, user);
                    break;
                case 2:
                    Shred(target, user);
                    break;
            }
        }

        public void WolfAI(Player target)
        {
            int AttNum = 1;
            string user = "Wolf";
            int ch = Randomizer(1, AttNum);

            switch (ch)
            {
                case 1:
                    Bite(target, user);
                    break;
                case 2:
                    Howl(target, user);
                    break;
                case 3:
                    Charge(target, user);
                    break;
            }
        }

        public void HarpyAI(Player target)
        {
            string user = "Harpy";
            Shred(target, user);
        }

        public void TrollAI(Player target)
        {
            int AttNum = 1;
            string user = "Troll";
            int ch = Randomizer(1, AttNum);

            switch (ch)
            {
                case 1:
                    Club(target, user);
                    break;
                case 2:
                    Throw(target, user);
                    break;
                case 3:
                    Charge(target, user);
                    break;

            }
        }

        public void DragonAI(Player target)
        {
            int AttNum = 5;
            string user = "Dragon";
            int ch = Randomizer(1, AttNum);

            switch (ch)
            {
                case 1:
                    Bite(target, user);
                    break;
                case 2:
                    Shred(target, user);
                    break;
                case 3:
                    Charge(target, user);
                    break;
                case 4:
                    Roar(target, user);
                    break;
                case 5:
                    Fire(target, user);
                    break;
            }
        }

        //Enemy Skills
        public void Bite(Player target, string user)
        {
            int damage = 0;
            target.health -= attack + damage;
            Console.WriteLine("{0} jumped at you and bit you for {1} damage!", user, (attack + damage));
        }
        public void Shred(Player target, string user)
        {
            int damage = 2;
            target.health -= attack + damage;
            Console.WriteLine("{0} ripped flesh from you body. You take {1} damage!", user, (attack * damage));
        }
        public void Howl(Player target, string user)
        {
        }
        public void Charge(Player target, string user)
        {
        }
        public void Club(Player target, string user)
        {
        }
        public void Throw(Player target, string user)
        {
        }
        public void Roar(Player target, string user)
        {
        }
        public void Fire(Player target, string user)
        {
            int damage = 25;
            if (target.res)
                Console.WriteLine("Dragon spits deadly fire at you from his throat! \nFire Ring protects you and you take 0 damage!");
            else
                Console.WriteLine("Dragon spits deadly fire at you from his throat! \nYou take {0} damage!", damage);
        }
    }
}
