using System;
using System.Collections.Generic;

namespace RPG
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
            int playerMagic = 10;
            bool res = false;
            // Enemy Indexes
            int giantRat = 0;
            int wolf = 1;
            int harpy = 2;
            int troll = 3;
            int dragon = 4;

            // Player creation
            Player hero = new Player(name, playerAtt, playerHp, playerMagic, res);
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
}
