using System;

namespace RPG
{
    public class Player : Creature
    {
        public Player(string _name, int _attack, int _health, int _magic, bool _res)
            : base(_name, _attack, _health, _magic, _res)
        {
        }

        // Level Up Method
        public void LevelUp(Player player)
        {
            int hp = 50;
            int att = 10;
            int mp = 10;

            Console.WriteLine("Level up!");
            Console.WriteLine("Attack:\n{0} --> {1}", player.attack, player.attack + att);
            Console.WriteLine("Health:\n{0} --> {1}", player.health, hp + player.health);
            Console.WriteLine("Magic:\n{0} --> {1}", player.magic, mp + player.magic);
            player.attack += att;
            player.health += hp;
            player.magic += mp;
        }

        // Standard attack
        public void Slash(Creature target)
        {
            target.health -= attack;
        }

        // Magic
        public void Heal(Player player)
        {
            int cost = 3;
            int heal = 50;
            player.health += heal;
            player.magic -= cost;
        }

        public void HeavyAttack(Enemy target, Player player)
        {
            int cost = 1;
            int multi = 2;
            target.health -= attack * multi;
            player.magic -= cost;
        }

        public void Flurry(Enemy target, Player player)
        {
            int cost = 1;
            target.health -= attack;
            player.magic -= cost;
        }

        // Methods used in Battle ----------------------------------------
        public int Choice(Player player) // Produces heros decision
        {
            bool correctInput = true;
            int choice = 0, choice2;
            while (correctInput)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Heal (3 Magic)");
                Console.WriteLine("3. Special");

                bool test = int.TryParse(Console.ReadLine(), out choice);
                if (!test || choice > 3 || choice <= 0)
                {
                    Console.WriteLine("Thats not one of the options! Try again!");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

                if (choice == 2 && player.magic < 2)
                {
                    Console.WriteLine("\nNot enough Magic!\n");
                    continue;
                }

                if (choice == 3) // Specials menu
                {
                    Console.WriteLine("Choose Special:\n1. Heavy Strike (1 Magic)\n2. Flurry (3 Magic)\n3. <--- Go back");
                    bool test2 = int.TryParse(Console.ReadLine(), out choice2);
                    if (!test2 || choice2 > 3 || choice2 <= 0)
                    {
                        Console.WriteLine("Thats not one of the options ! Try again!");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }

                    if (choice == 1 && player.magic < 1)
                    {
                        Console.WriteLine("\nNot enough Magic!\n");
                        continue;
                    }

                    if (choice == 2 && player.magic < 3)
                    {
                        Console.WriteLine("\nNot enough Magic!\n");
                        continue;
                    }

                    switch (choice2)
                    {
                        case 1:
                            choice = 4;
                            break;
                        case 2:
                            choice = 5;
                            break;
                    }
                }
                if (choice == 1 || choice == 2 || choice == 4 || choice == 5)
                    break;
            }
            return choice;
        }
        // Player Turn
        public void PlayerTurn(int decision, Enemy target, Player player)
        {
            switch (decision)
            {
                case 1:
                    Slash(target);
                    Console.WriteLine("You striked your enemy!");
                    break;
                case 2:
                    Heal(player);
                    Console.WriteLine("You healed for {0} health!", 15);
                    break;
                case 4:
                    HeavyAttack(target, player);
                    Console.WriteLine("You used heavy attack!");
                    break;
                case 5:
                    Flurry(target, player);
                    Console.WriteLine("You used Flurry!!!");
                    Flurry(target, player);
                    Console.WriteLine("You used Flurry!!!");
                    Flurry(target, player);
                    Console.WriteLine("You used Flurry!!!");
                    break;
            }
        }
    }
}
