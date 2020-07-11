using System;
using System.Collections.Generic;

namespace RPG_Project
{
    class Program
    {

        static void Main()
        {
            // Main Menu
            bool valid = true;
            Console.WriteLine("Welcome to Tiny RPG!\n\n1.Start Game\n2.Exit");
            while (valid)
            {
                string inp = Console.ReadLine();
                switch (inp)
                {
                    case "1": // Start the game
                        Story.Intro();
                        string playerName = Console.ReadLine();
                        Game(playerName);
                        break;
                    case "2": // Exit
                        valid = false;
                        break;
                    default: // Invalid input
                        Console.Clear();
                        Console.WriteLine("Please enter a valid option.\n1.Start Game\n2.Exit");
                        break;
                }
            }
        }

        static void Game(string name)
        {
            //Player initial statistics
            int playerAtt = 10;
            int playerHp = 100;
            bool res = false;
            // Enemy Indexes
            int giantRat = 0;
            int wolf = 1;
            int harpy = 2;
            int troll = 3;
            int dragon = 5;

            // Player creation
            Player hero = new Player(name, playerAtt, playerHp, res);
            // Enemy creation
            List<Enemy> enemyList = Enemy.EnemyContainer();
            // Story and battles

            //Road
            bool food = Story.RoadQuest(hero, name);
            Battle.Encounter(hero, enemyList[giantRat]);

            //Forest
            bool ally = Story.ForestQuest(food);
            if (ally == false)
                Battle.Encounter(hero, enemyList[wolf]);

            //Hills
            Story.HillsQuest();
            Battle.Encounter(hero, enemyList[harpy]);
            bool treasure = Story.HillsAfter(hero);

            //Bridge
            bool bridge = Story.BridgeQuest(treasure);
            if (bridge == true)
                Battle.Encounter(hero, enemyList[troll]);

            //Hidden Valley
            Story.HiddenValleyQuest(hero, food, ally, treasure);
            Battle.Encounter(hero, enemyList[dragon]);
            Story.Victory();
        }
    }

    public class Creature
    {
        public string name;
        public int attack;
        public int health;
        public bool res;

        public Creature(string _name, int _attack, int _health, bool _res)
        {
            name = _name;
            attack = _attack;
            health = _health;
            res = _res;
        }

        public void PrintStats()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("*            {0} stats:", name);
            Console.WriteLine("*        ");
            Console.WriteLine("*         Attack value is: {0}", attack);
            Console.WriteLine("*         Health value is: {0}", health);
            Console.WriteLine("---------------------------------------------");
        }

        public void NormAttack(Creature target)
        {
            target.health -= attack;
        }
    }

    public class Player : Creature
    {
        public int healLvl = 7, maxHealth = 15;

        public Player(string _name, int _attack, int _health, bool _res)
            : base(_name, _attack, _health, _res)
        {
        }

        public void LevelUp(int att, int hp, int hpLvl)
        {
            Console.WriteLine("Level up!");
            Console.WriteLine("Attack:\n{0} --> {1}", attack, att + attack);
            Console.WriteLine("Health:\n{0} --> {1}", maxHealth, hp + maxHealth);
            Console.WriteLine("Magic:\n{0} --> {1}", healLvl, hpLvl + healLvl);
            attack += att;
            maxHealth += hp;
            health = maxHealth;
            healLvl += hpLvl;
        }

        // Special -------------------------------------------
        public void Heal()
        {
            health += healLvl;

            if (health > maxHealth)
                health = maxHealth;
        }

        public void SpinAttack(Enemy target)
        {
            target.health -= ((attack - 2) * 3);
        }

        public void DoubleSlash(Enemy target)
        {
            target.health -= attack * 2;
        }

        // Methods used in Battle ----------------------------------------
        public int Choice() // Produces heros decision
        {
            bool correctInput = true;
            int choice = 0, choice2;
            while (correctInput)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Heal");
                Console.WriteLine("3. Special");

                bool test = int.TryParse(Console.ReadLine(), out choice);
                if (!test || choice > 3 || choice <= 0)
                {
                    Console.WriteLine("Thats not one of the options! Try again!");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

                if (choice == 3) // Specials menu
                {
                    Console.WriteLine("Choose Special:\n1. Spin Attack\n2. Double Slash\n3. <--- Go back");
                    bool test2 = int.TryParse(Console.ReadLine(), out choice2);
                    if (!test2 || choice2 > 3 || choice2 <= 0)
                    {
                        Console.WriteLine("Thats not one of the options ! Try again!");
                        Console.ReadLine();
                        Console.Clear();
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

        public void PlayerTurn(int decision, Enemy target)
        {
            switch (decision)
            {
                case 1:
                    NormAttack(target);
                    Console.WriteLine("You kicked the enemy!");
                    break;
                case 2:
                    Heal();
                    Console.WriteLine("You healed for {0} health!", healLvl);
                    break;
                case 4:
                    SpinAttack(target);
                    Console.WriteLine("You used spin attack!");
                    break;
                case 5:
                    DoubleSlash(target);
                    Console.WriteLine("You used double slash!");
                    break;
            }
        }
    }

    public class Enemy : Creature
    {
        public int numOfAttack;


        public Enemy(string _name, int _attack, int _health, bool _res)
            : base(_name, _attack, _health, _res)
        {

        }

        public static List<Enemy> EnemyContainer()
        {
            List<Enemy> enemies = new List<Enemy>();
            enemies.Add(new Enemy("Giant Rat", 1, 1, false));
            enemies.Add(new Enemy("Wolf", 1, 1, false));
            enemies.Add(new Enemy("Harpy", 1, 1, false));
            enemies.Add(new Enemy("Troll", 1, 1, false));
            enemies.Add(new Enemy("Dragon", 1, 1, false));
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

    class Battle
    {
        public static void DeathCheck(Player player)
        {
            if (player.health <= 0)
            {
                Console.Clear();
                Console.WriteLine("Defeat!\nTry again.\nPress any key to continue...");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        public static void PrintTheStats(Creature player)
        {
            player.PrintStats();
            Console.WriteLine("");
        }

        public static void Encounter(Player player, Enemy enemy)
        {
            while (enemy.health > 0 && player.health > 0)
            {
                PrintTheStats(player);
                player.PlayerTurn(player.Choice(), enemy);

                if (enemy.health > 0)
                {
                    enemy.EnemyTurn(enemy.name, player);
                    DeathCheck(player);
                }
            }
            Console.WriteLine("{0} was killed!", enemy.name);
            player.LevelUp(1, 1, 1);
            Console.ReadLine();
            Console.Clear();
        }
    }

    class Story
    {
        public static void Intro()
        {
            Console.Clear();
            Console.WriteLine("Your village was invaded by a terrible dragon!\nAll of your family and friends perished in flames.");
            Console.WriteLine("With nothing more to lose you decide to hunt down the beast or die trying.\nWhat is your name warrior?\n");
        }

        public static bool RoadQuest(Player player, string name)
        {
            bool valid = true;
            bool food = false;
            int helmet = 25;
            Console.WriteLine("{0}... That was the name given to you by your parents.\nParents killed for sport by a predator in the sky.", name);
            Console.WriteLine("There's no time to waste. This specific dragon in known for his tendencies\nto change his hunting grounds often.\n You need to travel light.");
            Console.WriteLine("You grab your rusty sword from your chest and need to decide to pack additional\nfood or an old iron helmet that once belonged to your father.");
            Console.WriteLine("What would you like to take?\n1.Food\n2.Helmet");
            while (valid)
            {
                string inp = Console.ReadLine();
                switch (inp)
                {
                    case "1":
                        food = false;
                        Console.WriteLine("Better safe than sorry. If you run out of food you won't be able to keep going.\nYou gain Bag of food.");
                        valid = false;
                        break;
                    case "2":
                        player.health += helmet;
                        Console.WriteLine("Dragon is a formidable beast. You will need all the protection you can get.\nYou gain 10 HP.");
                        valid = false;
                        break;
                    default: // Invalid input
                        Console.Clear();
                        Console.WriteLine("Please enter a valid option.\n1.Food\n2.Helmet");
                        break;
                }
            }
            Console.WriteLine("Without further delay you begin our quest.\nIt didn't take long before you find your first obstacle.\n Giant Rat appears!");
            return food;
        }
        public static bool ForestQuest(bool food)
        {
            bool valid = true;
            bool ally = false;
            Console.WriteLine("This rat was an easy prey. You trained for years to become a royal knight.\nFight didn't tire you so you decided to push further.");
            Console.WriteLine("When you reached the forest you saw a wolf. A big one. Most likely lured by the smell of blood that you were covered in from your last kill.");
            if (food)
            {
                while (valid)
                {
                    Console.WriteLine("It looks hungry. You could give him your spare food. Maybe it will leave you alone?");
                    Console.WriteLine("1. Give him food\n2. Attack the beast!");
                    string inp = Console.ReadLine();
                    switch (inp)
                    {
                        case "1":
                            Console.WriteLine("You have enough food and fighting this beast is unnecesarry risk.\nYou throw your spare food to the wolf.");
                            Console.WriteLine("He seems to like it and barks at you with grattiude.");
                            ally = true;
                            valid = false;
                            break;
                        case "2":
                            Console.WriteLine("You need that food and there's no guarantee that the wolf will leave you alone after his meal. You decide to attack.");
                            valid = false;
                            break;
                        default: // Invalid input
                            Console.Clear();
                            Console.WriteLine("Please enter a valid option.\n1. Give him food\n2. Attack the beast!");
                            break;
                    }
                }
            }
            else
                Console.WriteLine("There's no choice. You have to defend yourself.");
            return ally;
        }

        public static void HillsQuest()
        {
            Console.WriteLine("You've reached the hills. You are getting closer!");
            Console.WriteLine("Unfortunately there is a Harpy guarding the passage. You have to fight it to advance further.");
        }
        public static bool HillsAfter(Player player)
        {
            int sword = 5;
            bool valid = true;
            bool treasure = false;
            Console.WriteLine("After the battle you find a small opening in the wall that is close to collapsing. You can see a bag of gold and something shiny.");
            Console.WriteLine("If you take one of the item the wall opening will most likely collapse. What do you want to take?");
            Console.WriteLine("1. Bag of gold\n2. Something shiny");
            while (valid)
            {
                string inp = Console.ReadLine();
                switch (inp)
                {
                    case "1":
                        Console.WriteLine("You probably won't need it in your quest but who knows? Maybe there will encounter a traveling merchant on your way?");
                        treasure = true;
                        valid = false;
                        break;
                    case "2":
                        Console.WriteLine("A shiny thing was a Steel Sword!\n You gain +5 Attack!");
                        player.attack += sword;
                        valid = false;
                        break;
                    default: // Invalid input
                        Console.Clear();
                        Console.WriteLine("Please enter a valid option.\n1. Bag of gold\n2. Something shiny");
                        break;
                }
            }
            Console.WriteLine("After you reach your item the opening collapses. It's time to move on!");
            return treasure;
        }

        public static bool BridgeQuest(bool treasure)
        {
            bool valid = true;
            bool bridge = false;
            Console.WriteLine("It took some time to reach enormous bridge that you had to pass in order to reach the dragon.You've heard about it.");
            Console.WriteLine("Bridge made from iron and bones of poor travelers who could not pay the price of crossing");
            Console.WriteLine("Before you took a step forward you've heard troll shouting at you just behind you! 'TROLL TOLL!'");
            Console.WriteLine("You quickly turned around and saw the beast. How something of a size of an elephant moved behind you without making a sound?!");
            Console.WriteLine("This fight will not be easy...");
            if (treasure)
            {
                Console.WriteLine("You could give him the treasure you found earlier on. He would let you pass.");
                Console.WriteLine("1. Give him the treasure\n2. Fight the beast!");
                while (valid)
                {
                    string inp = Console.ReadLine();
                    switch (inp)
                    {
                        case "1":
                            Console.WriteLine("There's no time to waste and there's no reason to hold on to this heavy bag of gold. You throw it at Troll.");
                            Console.WriteLine("'YOU PASS!'\nWithout further delay you cross the bridge.");
                            valid = false;
                            break;
                        case "2":
                            Console.WriteLine("How many innocent people did he kill? This stops today. You charge at him without hesitation.");
                            bridge = true;
                            valid = false;
                            break;
                        default: // Invalid input
                            Console.Clear();
                            Console.WriteLine("Please enter a valid option.\n1. Give him the treasure\n2. Fight the beast!");
                            break;
                    }
                }
            }

            return bridge;
        }

        public static void HiddenValleyQuest(Player player, bool ally, bool treasure, bool food)
        {
            int foodHealth = 10;
            Console.WriteLine("You can hear the dragon in the distance. It's time to prepare for final encounter.");
            if (food)
            {
                Console.WriteLine("The journey was long and exhausting. It's time to make use of extra food you brought.");
                Console.WriteLine("Heath replenished!\nMax health + 10!");
                player.maxHealth += foodHealth;
                player.health = player.maxHealth;
            }
            if (treasure)
            {
                Console.WriteLine("You decide to leave your bag of treasure behind. It will only make things more difficult if you were to carry it with you.");
                Console.WriteLine("When it fell on the ground a red ring fell from it. You can see an aura of magic pulsing from it.");
                Console.WriteLine("You pick it up and equip it.\nFire Ring equiped. Fire resistance gained!");
                player.res = true;
            }
            if (ally)
            {

            }
        }
        public static void Victory()
        {
            Console.WriteLine("");
        }

    }
}
