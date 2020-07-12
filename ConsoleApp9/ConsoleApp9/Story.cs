using System;

namespace RPG
{
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
            Console.Clear();
            Console.WriteLine("{0}... That was the name given to you by your parents.\nParents killed for sport by a predator in the sky.", name);
            Console.WriteLine("There's no time to waste. This specific dragon in known for his tendencies\nto change his hunting grounds often.\nYou need to travel light.\n");
            Console.WriteLine("You grab your rusty sword from your chest and need to decide to pack additional\nfood or an old iron helmet that once belonged to your father.");
            Console.WriteLine("\nWhat would you like to take?\n1.Food\n2.Helmet");
            while (valid)
            {
                string inp = Console.ReadLine();
                switch (inp)
                {
                    case "1":
                        food = true;
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
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Without further delay you begin our quest.\nIt didn't take long before you find your first obstacle.\n\nGiant Rat appears!");
            return food;
        }
        public static bool ForestQuest(bool food)
        {
            bool valid = true;
            bool ally = false;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
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
                            Console.WriteLine("He seems to like it and barks at you with gratitude. After that he runs off.");
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
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You've reached the hills. You are getting closer!");
            Console.WriteLine("Unfortunately there is a Harpy guarding the passage. You have to fight it to advance further.");
        }
        public static bool HillsAfter(Player player)
        {
            int sword = 5;
            bool valid = true;
            bool treasure = false;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
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
                        Console.WriteLine("A shiny thing was a Steel Sword!\nYou gain +5 Attack!");
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
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            return treasure;
        }

        public static bool BridgeQuest(bool treasure)
        {
            bool valid = true;
            bool bridge = false;
            Console.WriteLine("It took some time to reach enormous bridge that you had to pass in order to reach the dragon.You've heard about it.");
            Console.WriteLine("Bridge made from iron and bones of poor travelers who could not pay the price of crossing");
            Console.WriteLine("Before you took a step forward you've heard troll shouting at you just behind you! 'TROLL TOLL!'");
            Console.WriteLine("You quickly turned around and saw the beast. How something of a size of an elephant\nmoved behind you without making a sound?!");
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
            int sword = 25;
            Console.WriteLine("You can hear the dragon in the distance. It's time to prepare for final encounter.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            if (food)
            {
                Console.WriteLine("The journey was long and exhausting. It's time to make use of extra food you brought.");
                Console.WriteLine("Heath replenished!\nMax health + 10!");
                player.health += foodHealth;
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            if (treasure)
            {
                Console.WriteLine("You decide to leave your bag of treasure behind. It will only make things more difficult if you were to carry it with you.");
                Console.WriteLine("When it fell on the ground a red ring fell from it. You can see an aura of magic pulsing from it.");
                Console.WriteLine("You pick it up and equip it.\nFire Ring equiped. Fire resistance gained!");
                player.res = true;
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            if (ally)
            {
                Console.WriteLine("You hear a howl in a distance.\nYou can see a wolf you fed before holding somehting in his mouth.");
                Console.WriteLine("He drops it under your feet and runs off. You recognize the sword from fairytail books you were once read by your parents.");
                Console.WriteLine("It's a legendary sword that was used to slay a dragon king hunders of years ago!");
                Console.WriteLine("Dragon Slayer sword gained!\n+25 Damage!");
                player.attack += sword;
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine("You are ready.\nYou walk towards hidden valley where the beast is waiting for you.\n\nDragon appears!");
        }
        public static void Victory()
        {
            Console.WriteLine("Victory!\nYou defeated the dragon and avenged your village!\nYou decide to travel to the kingdom and become a knight to protect the innocent.");
            Console.WriteLine("Press Enter key to continue...");
        }

    }
}
