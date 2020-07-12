using System;

namespace RPG
{
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
        public static void Encounter(Player player, Enemy enemy)
        {
            while (enemy.health > 0 && player.health > 0)
            {
                PrintStats(player, enemy);
                player.PlayerTurn(player.Choice(), enemy, player);

                if (enemy.health > 0)
                {
                    enemy.EnemyTurn(enemy.name, player);
                    DeathCheck(player);
                }
            }
            Console.WriteLine("{0} was killed!", enemy.name);
            player.LevelUp(player);
        }

        public static void PrintStats(Player player, Enemy enemy)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("            {0} stats:\t\t\t*\t\t{1} stats:", player.name, enemy.name);
            Console.WriteLine("            Attack value is: {0}\t\t*\t\tAttack value is: {1}", player.attack, enemy.attack);
            Console.WriteLine("            Health value is: {0}\t*\t\tHealth value is: {1}", player.health, enemy.health);
            Console.WriteLine("            Magic value is: {0}\t\t*", player.magic);
            Console.WriteLine("-----------------------------------------------------------------------------------------");
        }
    }
}
